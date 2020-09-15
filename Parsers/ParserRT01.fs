namespace Parsers
open System
open FSharp.Collections.ParallelSeq

module ParserRT01 =
    type CommunicationPathsEnum = RT | BC | Error
    type ErorType = NoResp | Normal

    type RT01 =
        {
        Time       : uint64
        RT1        : uint32
        SA1        : uint8 
        Status1    : uint16
        RT2        : uint32
        SA2        : uint32
        Status2    : uint16
        DataSource : uint32
        Bus        : char
        Path       : CommunicationPathsEnum
        Resp2      : uint16
        Words      : uint16 list 
        Error      : ErorType
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
        else null

    let TryParseUInt16 x = 
        try 
            x |> (fun x -> Convert.ToUInt16(x, 16))  |> Some
        with :? FormatException -> 
            None
    
    let countCharFromNth (getStr : string)(chkdChar : char) = 
        let rec loop i count =
            if i < getStr.Length then 
                if getStr.[i] = chkdChar then loop (i+1) (count+1)
                else loop (i+1) count
            else count
        loop 0 0

    let result x:uint16 option = 
        match x with 
        | Some i -> Some(i)
        | None -> None
    
    let IsNotNone x = 
        x<>None
    
    let TimeStringToUInt64() (input:string) = 
        let x = input.Split([|':'|], StringSplitOptions.RemoveEmptyEntries)
                |> Seq.map (fun x -> x |> uint64)
                |> Seq.toList
        let out:uint64 = ( (x.[0]*(3600UL) + x.[1]*(60UL) + x.[2] ) * (1_000_000UL) + x.[3] |> uint64)
        out 

    let SetElementDataRecordRT01 (aList: string list) (count : int)= 
           let currentElement = 
               {
                   Time       = TimeStringToUInt64() aList.[2]
                   RT1        = aList.[3] |> uint32
                   SA1        = aList.[4] |> uint8
                   Status1    = FilterByParameter aList "status1:"  |> uint16
                   RT2        = FilterByParameter aList "rt2:"      |> uint32
                   SA2        = FilterByParameter aList "sa2:"      |> uint32
                   Status2    = FilterByParameter aList "status2:"  |> uint16
                   DataSource = aList.[9]  |> uint32
                   Bus        = aList.[10].[0] 
                                |> char
                   Path       = match  aList.[11]  with 
                                | "RT->BC" -> CommunicationPathsEnum.RT
                                | "BC->RT" -> CommunicationPathsEnum.BC
                                | _ -> CommunicationPathsEnum.Error
                   Resp2      = aList.[12].[0..aList.[12].Length-3] |> uint16
                   Words      = let x =  aList.[13..count-1]  
                                         |> Seq.map (fun x -> (TryParseUInt16 x))
                                         |> Seq.filter IsNotNone
                                         |> Seq.map (fun x -> x.Value |> uint16)
                                         |> Seq.toList
                                x
                   Error      =     let x = aList.[count..aList.Length] 
                                            |> Seq.map (fun x -> x + " ")
                                            |> String.Concat  
                                    match x with
                                    | "error code: no response " -> ErorType.NoResp
                                    | _ -> ErorType.Normal      
               }
           currentElement

    let GetDataByString (inputString:string) =
        let inputList = inputString.Split([|"\r"|], StringSplitOptions.RemoveEmptyEntries)
                        |> Seq.toList 
        let countOfDelimeters = countCharFromNth inputList.[0] ',' + 4//MAGIC
        let Blocks = 
            inputList.[1..inputList.Length-2]//excluding the part with names and the last empty(tab) line
            |> PSeq.map (fun x -> x.Split([|", \t";",\t";", ";",";" "|], StringSplitOptions.RemoveEmptyEntries))
            |> PSeq.map (fun x -> x |> Seq.toList)
            |> PSeq.map (fun x -> SetElementDataRecordRT01 x countOfDelimeters)
            |> PSeq.toList
            |> List.sortBy (fun (x : RT01) -> x.Time) 
        Blocks

    let GetDataByPath (path:string) = 
       let inputString = System.IO.File.ReadAllText path
       let outputList = GetDataByString inputString
       outputList

