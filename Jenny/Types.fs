namespace Jenny

type Err =
    | RowsNotSameLengths
    | EmptyList

type Element =
    internal
    | Heading1 of string
    | Heading2 of string
    | Heading3 of string
    | Heading4 of string
    | Heading5 of string
    | Heading6 of string
    | Paragraph of string
    | CodeBlock of string
    | BlockQuote of string
    | Table of list<list<string>>
    | OrderedList of list<string>
    | UnorderedList of list<string>

module Element =
    let heading1 text = Element.Heading1 text
    let heading2 text = Element.Heading2 text
    let heading3 text = Element.Heading3 text
    let heading4 text = Element.Heading4 text
    let heading5 text = Element.Heading5 text
    let heading6 text = Element.Heading6 text
    let paragraph text = Element.Paragraph text
    let codeBlock text = Element.CodeBlock text
    let blockQuote text = Element.BlockQuote text

    let table (colNames : list<string>) (rows : list<list<string>>) : Result<Element, Err> =
        let colNamesLen = List.length colNames
        let emptyRows = 
            rows
            |> List.filter (fun row -> List.length <| row = 0)
            |> List.length
            |> (fun len -> if len = 0 then false else true)
        if not (colNamesLen = 0 || List.length <| rows = 0 || emptyRows) then
            let rowsSameLength =
                rows
                    |> List.forall (fun row -> List.length row = colNamesLen)
            match rowsSameLength with
            | true -> Ok(Element.Table (colNames :: rows))
            | false -> Error(Err.RowsNotSameLengths)
        else
            Error(Err.EmptyList)

    let orderedList (rows : list<string>) : Result<Element, Err> =
        match List.length rows with
        | 0 -> Error(Err.EmptyList)
        | _ -> Ok(Element.OrderedList rows)

    let unorderedList (rows : list<string>) : Result<Element, Err> =
        match List.length rows with
        | 0 -> Error(Err.EmptyList)
        | _ -> Ok(Element.UnorderedList rows)
