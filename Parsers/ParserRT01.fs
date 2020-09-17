namespace Parsers

open System
open System.IO
open System.Threading.Tasks
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

    let SetElementDataRecordRT01 (aList: string[])= 
           let currentElement = 
               {
                   Time       = TimeStringToUInt64() aList.[2]
                   RT1        = aList.[3] |> Convert.ToUInt32 
                   SA1        = aList.[4] |> uint8
                   Status1    = FilterByParameter aList "status1:"  |> uint16
                   RT2        = FilterByParameter aList "rt2:"      |> Convert.ToUInt32 
                   SA2        = FilterByParameter aList "sa2:"      |> Convert.ToUInt32 
                   Status2    = FilterByParameter aList "status2:"  |> uint16
                   DataSource = aList.[9]  |> uint32
                   Bus        = aList.[10].[0] 
                                |> char
                   Path       = match  aList.[11]  with 
                                | "RT->BC" -> CommunicationPathsEnum.RT
                                | "BC->RT" -> CommunicationPathsEnum.BC
                                | _ -> CommunicationPathsEnum.Error
                   Resp2      = aList.[12].[0..aList.[12].Length-3] |> uint16
                   Words      = let x =  aList.[13..aList.Length-2]  
                                         |> Seq.filter (fun x -> x.Length=4)
                                         |> Seq.map (fun x -> Convert.ToUInt16(x, 16))
                                         |> Seq.toList
                                x
                   Error      = let x = aList.[aList.Length-4..aList.Length] 
                                        |> Seq.map (fun x -> x + " ")
                                        |> String.Concat  
                                match x with
                                | "error code: no response " -> ErorType.NoResp
                                | _ -> ErorType.Normal      
               }
           currentElement
    
    let readLines (filePath:string) = seq  {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield sr.ReadLine ()
    }

    let GetDataByString (inputString:string) =
            inputString 
            |> (fun x -> x.Split([|", \t";",\t";", ";",";" "|], StringSplitOptions.RemoveEmptyEntries))
            |> (fun x -> SetElementDataRecordRT01 x) 
            
    let GetDataByPath (path:string) = 
            readLines(path)
            |> Seq.skip 1
            |> Seq.takeWhile(fun x -> x <> "\t")
            |> PSeq.map GetDataByString
            |> PSeq.sortBy(fun (x : RT01) -> x.Time)
            |> PSeq.toArray

