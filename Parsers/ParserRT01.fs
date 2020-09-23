namespace Parsers

open System
open System.IO
open FSharp.Collections.ParallelSeq
open Medusa.Analyze1553B.Common

module ParserRT01 =

    let Contain (contain : string) = 
        (fun x -> x.ToString().Contains(contain)) 

    let FilterByParameter (list : string[]) parameter  =
        Array.filter (Contain parameter) list 
                         |> Array.map(fun x -> x.Trim())
                         |> Array.map(fun x -> x.Split([|parameter|], StringSplitOptions.RemoveEmptyEntries) )
                         |> Array.map(fun x -> x.[0] )
                         |> Array.head
                         

    let TimeStringToUInt64() (input:string) = 
        let x = input.Split([|':'|], StringSplitOptions.RemoveEmptyEntries)
                |> Seq.map (fun x -> x |> uint64)
                |> Seq.toList
        let out:uint64 = ( (x.[0]*(3600UL) + x.[1]*(60UL) + x.[2] ) * (1_000_000UL) + x.[3])
        out 

    let TimeStringToFloat() (input:string) = 
           let x = input.Split([|':'|], StringSplitOptions.RemoveEmptyEntries)
                   |> Seq.map (fun x -> x |> float)
                   |> Seq.toList
           let out:float = ( (x.[0]*(3600.0) + x.[1]*(60.0) + x.[2] ) * (1_000_000.0) + x.[3])
                            |> float
           out 

    let GetTime (input : string[]) =
        Nullable(TimeStringToFloat() input.[2])  
           
    let GetChannel (input : string[]) =
           match input.[10].[0] with                                           
           | 'A' -> Nullable(BusChannel.A)                   
           | 'B' -> Nullable(BusChannel.B)                   
           | _   -> Nullable()  
           
    let GetError (input : string[]) =
           let x = input.[input.Length-4..input.Length]                        
                   |> Seq.map (fun x -> x + " ")            
                   |> String.Concat                         
           match x with                                                        
           | "error code: no response " -> Nullable(Error.NoResp)              
           | _ -> Nullable()  
           
    let GetCW1 (input : string[]) = 
         let address = input.[3] |> int
         let subaddress = input.[4] |> int
         let direction = 
             match  input.[11]  with 
             | "RT->BC" -> DataDirection.R
             | "BC->RT" -> DataDirection.T
             | _ -> DataDirection.R
         let length = 
             let x =  input.[13..input.Length-2]  
                      |> Array.filter (fun x -> x.Length=4)
             x.Length
         let status1 = (FilterByParameter input "status1:"  |> uint16)
         Nullable(ControlWord(address,direction,subaddress,length))
            
    let GetCW2 (input : string[]) =
        let x = FilterByParameter input "status2:"  |> uint16               
        match x with                                                        
        | 0us -> Nullable()                                                 
        | x -> Nullable(ControlWord(x))      

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
        input.[13..input.Length-2]                                            
        |> Seq.filter (fun x -> x.Length=4)                                   
        |> Seq.map (fun x -> Convert.ToUInt16(x, 16))                         
        |> Seq.toArray                                                        

    let GetDataRecordRT01 (inputRow: string[]) =
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

    let readLines (filePath:string) = seq  {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield sr.ReadLine ()
    }

    let GetDataByString (inputString:string) =
            inputString 
            |> (fun x -> x.Split([|", \t";",\t";", ";",";" "|], StringSplitOptions.RemoveEmptyEntries))
            |> (fun x -> GetDataRecordRT01 x) 
            
    let GetDataByPath (path:string) = 
            readLines(path)
            |> Seq.skip 1
            |> Seq.takeWhile(fun x -> x <> "\t")
            |> PSeq.map GetDataByString
            |> PSeq.sortBy(fun (x : DataRecord) -> x.MonitorTime)
            |> PSeq.toArray

