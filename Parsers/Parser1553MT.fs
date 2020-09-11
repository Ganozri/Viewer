namespace Parsers
open System
open FSharp.Collections.ParallelSeq

module Parser1553MT =

    type DataRecord1553MT =
        {
        Time   : string
        BusTime: int
        Bus    : char
        Cmd1   : string
        Cmd2   : string
        status1: uint16
        status2: uint16
        resp1  : uint16
        resp2  : uint16
        words  : uint16 list
        }

    
    let splitWithoutDelete inputString splitArg = 
         inputString
         |> List.map (fun x -> splitArg + x)
    
    let func (contain : string) = 
         (fun x -> x.ToString().Contains(contain)) 
    
    let filterByParameter list parameter =
         let x:string list = List.filter (func parameter) list
         if x.Length > 0 then 
             let y = x.Head.Split([|parameter; ":"; "\r" |], StringSplitOptions.RemoveEmptyEntries)
             let firstSymbol = y.[0]
             if (firstSymbol).[0] = ' ' 
             then firstSymbol.[1..firstSymbol.Length-1] 
             else firstSymbol
         else "MISSED PARAMETER"
    
    let breakIntoBlocks (blocks:string)  =
         let a = blocks.Split([|"     ";"         ";"    -->    "; "    <--    ";"  ";"\n";"\r"|], StringSplitOptions.RemoveEmptyEntries)
         let aList = Seq.toList a
         aList
    
    let outputValue (x:string) =
        match x.[x.Length-2..x.Length-1] with
        | "ms" -> x.[0..x.Length-3] 
                |> int 
        | "us" -> x.[0..x.Length-3] 
                |> int 
        | _ -> x|> int

    let outputUintValue (x:string) =
         match x.[x.Length-2..x.Length-1] with
         | "ms" -> x.[0..x.Length-3] 
                 |> uint16 
         | "us" -> x.[0..x.Length-3] 
                 |> uint16 
         | _ -> x|> uint16
    
    let SetElementDataRecord1553MT (aList:string list) = 
        let currentElement = 
            {
                Time    = aList.[1].Split([|" "|], StringSplitOptions.RemoveEmptyEntries).[1]
                BusTime = outputValue (filterByParameter aList "BusTime")
                Bus     = filterByParameter aList "Bus:" 
                          |> (fun x -> x.[0])
                Cmd1    = filterByParameter aList "Cmd1"
                Cmd2    = filterByParameter aList "Cmd2"
                status1 = outputUintValue (filterByParameter aList "status1")
                status2 = outputUintValue (filterByParameter aList "status2")
                resp1   = outputUintValue (filterByParameter aList " resp1")
                resp2   = outputUintValue (filterByParameter aList "resp2:")
                words   = (aList.[10..(aList.Length - 1)] 
                          |> String.Concat).Split([|" "; ";"; "error code: no response ," |], StringSplitOptions.RemoveEmptyEntries) 
                          |> Seq.map (fun x -> Convert.ToUInt16(x, 16))
                          |> Seq.toList
            }
        currentElement
    
    let GetDataByString (inputString:string) =
         let result = inputString.Split([|"MsgNO"|], StringSplitOptions.RemoveEmptyEntries)
         let listResult = Seq.toList result
         let blocks = splitWithoutDelete listResult "MsgNO"
         let xSeq = [for i in 0..blocks.Length-1 do SetElementDataRecord1553MT (breakIntoBlocks blocks.[i])]
         xSeq
    
    let GetDataByPath (path:string) = 
     let inputString = System.IO.File.ReadAllText path
     let xSeq = GetDataByString inputString
     xSeq
    
    let NewGetDataByPath (path:string) = 
        let inputString = System.IO.File.ReadAllText path
        let result = inputString.Split([|"MsgNO"|], StringSplitOptions.RemoveEmptyEntries)
        let listResult = Seq.toList result
        let blocks = splitWithoutDelete listResult "MsgNO"

        let startTime = System.Diagnostics.Stopwatch.StartNew()
        let LengthOfBlocks = blocks.Length-1
        //let FinalBlocks = [for i in 0..LengthOfBlocks/2 do  breakIntoBlocks blocks.[i]]
        //let FinalBlocks = [for i in LengthOfBlocks/2..LengthOfBlocks do  breakIntoBlocks blocks.[i]]
        let newList = 
            blocks
            |> PSeq.map breakIntoBlocks
            |> PSeq.map SetElementDataRecord1553MT
            |> PSeq.toList
        newList
    

    //[<EntryPoint>]
    //let main argv =

    //    let startTime = System.Diagnostics.Stopwatch.StartNew()

    //    //let Data = GetDataByPath(@"D:\Data\1553-MT.txt")
    //    let Data = GetDataByPath(@"D:\Data\SMALL.txt")

    //    startTime.Stop()
    //    printfn "\nTime = %A" (startTime.Elapsed.TotalMilliseconds) 

    //    0 


