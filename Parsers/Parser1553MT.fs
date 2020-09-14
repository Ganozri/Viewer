namespace Parsers
open System
open FSharp.Collections.ParallelSeq

module Parser1553MT =
    type DataRecord1553MT =
        {
        MsgNO  : int
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

    let Contain (contain : string) = 
         (fun x -> x.ToString().Contains(contain)) 
    
    let FilterByParameter list parameter =
         let x:string list = List.filter (Contain parameter) list
         if x.Length > 0 then 
             let y = x.Head.Split([|parameter; ":"; "\r" |], StringSplitOptions.RemoveEmptyEntries)
             let firstSymbol = y.[0]
             if (firstSymbol).[0] = ' ' 
             then firstSymbol.[1..firstSymbol.Length-1] 
             else firstSymbol
         else "MISSED PARAMETER"
    
    let BreakIntoBlocks (blocks:string)  =
         let a = blocks.Split([|"     ";"         ";"    -->    "; "    <--    ";"  ";"\n";"\r"|], StringSplitOptions.RemoveEmptyEntries)
         let aList = Seq.toList a
         aList
    
    let ToInt (x:string) = x.[0..x.Length-3] |> int

    let ToUint (x:string) = x.[0..x.Length-3] |> uint16
    
    let SetElementDataRecord1553MT (aList:string list) = 
        let currentElement = 
            {
                MsgNO   = aList.[0].[6..aList.[0].Length] |>int
                Time    = aList.[1].Split([|" "|], StringSplitOptions.RemoveEmptyEntries).[1] 
                BusTime = ToInt (FilterByParameter aList "BusTime") 
                Bus     = FilterByParameter aList "Bus:" |> (fun x -> x.[0])
                Cmd1    = FilterByParameter aList "Cmd1"
                Cmd2    = FilterByParameter aList "Cmd2"
                status1 = ToUint (FilterByParameter aList "status1")
                status2 = ToUint (FilterByParameter aList "status2")
                resp1   = ToUint (FilterByParameter aList " resp1")
                resp2   = ToUint (FilterByParameter aList "resp2:")
                words   = (aList.[10..(aList.Length - 1)] 
                          |> String.Concat).Split([|" "; ";"; "error code: no response ," |], StringSplitOptions.RemoveEmptyEntries) 
                          |> Seq.map (fun x -> Convert.ToUInt16(x, 16))
                          |> Seq.toList
            }
        currentElement
    
    let GetDataByString (inputString:string) =
         let listResult = 
             inputString.Split([|"MsgNO"|], StringSplitOptions.RemoveEmptyEntries)
             |> Seq.toList 
         let blocks = 
            listResult
            |> List.map (fun x -> "MsgNO" + x)//Add MsgNO after Split
            |> PSeq.map BreakIntoBlocks
            |> PSeq.map SetElementDataRecord1553MT
            |> PSeq.toList
            |>List.sortBy (fun (x : DataRecord1553MT) -> x.MsgNO) 

         blocks
    
    let GetDataByPath (path:string) = 
        let inputString = System.IO.File.ReadAllText path
        let outputList = GetDataByString inputString
        outputList



