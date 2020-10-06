namespace Parsers

open System
open System.IO
open System.Threading.Tasks
open FSharp.Collections.ParallelSeq
open Medusa.Analyze1553B.Common
open System.Text


module ParserRT01 =

  
    let rec len = function
    | [] -> 0
    | _::t -> 1 + (len t)

    [<EntryPoint>]
    let main argv =
        let x = (+) 1 2 
        let list = [1..10]
        let len = len list
        printf "%A" len
        0 
    