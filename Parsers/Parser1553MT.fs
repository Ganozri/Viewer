namespace Parsers
open System

module Parser1553MT =
    type DataRecord1553MT =
        {
        MsgNO  : string
        Time   : string
        BusTime: float
        Bus    : string
        Cmd1   : string
        Cmd2   : string
        status1: string
        status2: string
        resp1  : float
        resp2  : float
        words  : string list
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

    let breakIntoBlocks (blocks:string list) (count:int) =
        let a = blocks.[count].Split([|"     ";"         ";"    -->    ";"  ";"\n";"\r"|], StringSplitOptions.RemoveEmptyEntries)
        let aList = Seq.toList a
        aList
    
    let outputValue (x:string) =
        match x.[x.Length-2..x.Length-1] with
        | "ms" -> x.[0..x.Length-3] 
                |> float 
                |> (fun n -> n / 1000.0)
        | "us" -> x.[0..x.Length-3] 
                |> float 
                |> (fun n -> n / 1000000.0)
        | _ -> 666.666

    let SetElementDataRecord1553MT (aList:string list) = 
        let currentElement = 
            {
                MsgNO   = filterByParameter aList "MsgNO"
                Time    = aList.[1].Split([|"\n";"\r"|], StringSplitOptions.RemoveEmptyEntries).[0]
                BusTime = outputValue (filterByParameter aList "BusTime")
                Bus     = filterByParameter aList "Bus:"
                Cmd1    = filterByParameter aList "Cmd1"
                Cmd2    = filterByParameter aList "Cmd2"
                status1 = filterByParameter aList "status1"
                status2 = filterByParameter aList "status2"
                resp1   = outputValue (filterByParameter aList " resp1")
                resp2   = outputValue (filterByParameter aList "resp2:")
                words   = aList.[10..aList.Length - 1]
            }
        currentElement
    
    let GetDataByString (inputString:string) =
        let result = inputString.Split([|"MsgNO"|], StringSplitOptions.RemoveEmptyEntries)
        let listResult = Seq.toList result
        let blocks = splitWithoutDelete listResult "MsgNO"
        let xSeq =
            seq {
                    for i in 0..blocks.Length-1 do
                    yield! seq { SetElementDataRecord1553MT (breakIntoBlocks blocks i)}
                }
        xSeq

    let GetDataByPath (path:string) = 
        let inputString = System.IO.File.ReadAllText path
        let xSeq = GetDataByString inputString
        xSeq


