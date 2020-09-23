namespace Parsers
open System
open FSharp.Collections.ParallelSeq
open Medusa.Analyze1553B.Common

module Parser1553MT =

    let Contain (contain : string) = 
         (fun x -> x.ToString().Contains(contain)) 

    let FilterByParameter (list : string[]) parameter  =
        let x = Array.filter (Contain parameter) list 
        if x.Length > 0 then
        x
        |> Array.map(fun x -> x.Trim())
        |> Array.map(fun x -> x.Split([|parameter|], StringSplitOptions.RemoveEmptyEntries) )
        |> Array.map(fun x -> x.[0] )
        |> Array.head
        else ""
     
    let BreakIntoBlocks (blocks:string)  =
        let a = blocks.Split([|"     ";"         ";"    -->    "; "    <--    ";"  ";"\n";"\r"|], StringSplitOptions.RemoveEmptyEntries)
        let aList = Seq.toArray a
        aList
    
    let ToUint32 (x:string) = x.[0..x.Length-3] |> uint32

    let ToUint (x:string) = x.[0..x.Length-3] |> uint16

    let GetTime (input : string[]) =
       let RawBusTime = FilterByParameter input "BusTime:"
       let BusTimeWithoutText = RawBusTime.[0..RawBusTime.Length-3]  |> float
       Nullable(BusTimeWithoutText)  
          
    let GetChannel (input : string[]) =
          let x = FilterByParameter input "Bus:" |> (fun x -> x.[0])
          match x with                                           
          | 'A' -> Nullable(BusChannel.A)                   
          | 'B' -> Nullable(BusChannel.B)                   
          | _   -> Nullable()  
          
    let GetError (input : string[]) =
           let x = FilterByParameter input "error code: "
           match x with
           | null -> Nullable()
           | "" -> Nullable()
           | "no response ," -> Nullable(Error.NoResp)
           | _ -> Nullable(Error.NoResp)
           
    let GetCW1 (input : string[]) = 
         let RawCmd1 = (FilterByParameter input "Cmd1:").Split([|" "; "-";|], StringSplitOptions.RemoveEmptyEntries) 
         let address = RawCmd1.[0] |> int
         let subaddress = RawCmd1.[2] |> int
         let direction = match RawCmd1.[1] with
         | "RX" -> (DataDirection.R)
         | "TX" -> (DataDirection.T)
         | _ ->   (DataDirection.R)
         let length = RawCmd1.[3] |> Convert.ToInt32
         Nullable(ControlWord(address,direction,subaddress,length))
            
    let GetCW2 (input : string[]) =
          let RawCmd2 = (FilterByParameter input "Cmd2: ").Split([|" "; "-";|], StringSplitOptions.RemoveEmptyEntries) 
          let address = RawCmd2.[0] |> int
          let subaddress = RawCmd2.[1] |> int
          let length = RawCmd2.[2] |> Convert.ToInt32
          if address = 0 && subaddress = 0 && length = 0 then Nullable()
          else Nullable(ControlWord(address,(GetCW1 input).Value.Direction,subaddress,length))  

    let GetResponseWord1 (input : string[]) =
        let status1 = (FilterByParameter input "status1:"  |> uint16)
        match status1 with
        | 0us -> Nullable()
        | x -> Nullable(ResponseWord(x))

    let GetResponseWord2 (input : string[]) =
        let status2 = (FilterByParameter input "status2:"  |> uint16)
        match status2 with
        | 0us -> Nullable()
        | x -> Nullable(ResponseWord(x))


    let GetDataWords (input : string[]) =
        (input.[10..(input.Length - 1)] 
        |> String.Concat).Split([|" "; ";"; "error code: no response ," |], StringSplitOptions.RemoveEmptyEntries) 
        |> Seq.map (fun x -> Convert.ToUInt16(x, 16))
        |> Seq.toArray                                                        

    let GetDataRecord1553MT (inputRow: string[]) =
        let builder = new DataRecordBuilder(
            Nullable(),                //Index
            GetTime          inputRow, //MonitorTime
            GetChannel       inputRow, //Channel
            GetError         inputRow, //Error             
            GetCW1           inputRow, //ControlWord1
            GetCW2           inputRow, //ControlWord2
            GetResponseWord1 inputRow, //ResponseWord1
            GetResponseWord2 inputRow, //ResponseWord2
            GetDataWords     inputRow  //Data
        )
        builder.GetRecord()

    let GetDataByString (inputString:string) =
         let listResult = 
             inputString.Split([|"MsgNO"|], StringSplitOptions.RemoveEmptyEntries)
             |> Seq.toArray 
         let Blocks = 
            listResult
            |> PSeq.map (fun x -> "MsgNO" + x)
            |> PSeq.map BreakIntoBlocks
            |> PSeq.map GetDataRecord1553MT
            |> PSeq.toArray
            |> Array.sortBy (fun (x : DataRecord) -> x.MonitorTime) 
         Blocks
    
    let GetDataByPath (path:string) = 
        let inputString = System.IO.File.ReadAllText path
        let outputList = GetDataByString inputString
        outputList



