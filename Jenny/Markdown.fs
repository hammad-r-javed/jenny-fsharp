namespace Jenny

module Markdown =
    open System.Collections.Generic
    
    let private escapeCharMap = 
        Map [
            ('\\', "\\\\");
            ('`', "\\`");
            ('*', "\\*");
            ('_', "\\_");
            ('{', "\\{");
            ('}', "\\}");
            ('[', "\\[");
            (']', "\\]");
            ('(', "\\(");
            (')', "\\)");
            ('#', "\\#");
            ('+', "\\+");
            ('-', "\\-");
            ('.', "\\.");
            ('!', "\\!");
        ]

    let private escapeChar char =
        try
            Map.find char escapeCharMap
        with
            | :? KeyNotFoundException -> $"{char}"

    let private escapeSpecialChars text =
        text
        |> String.collect escapeChar

    let encode element =
        match element with
        | Element.Heading1 text -> $"# {escapeSpecialChars text}\n\n"
        | Element.Heading2 text -> $"## {escapeSpecialChars text}\n\n"
        | Element.Heading3 text -> $"### {escapeSpecialChars text}\n\n"
        | Element.Heading4 text -> $"#### {escapeSpecialChars text}\n\n"
        | Element.Heading5 text -> $"##### {escapeSpecialChars text}\n\n"
        | Element.Heading6 text -> $"###### {escapeSpecialChars text}\n\n"
        | Element.Paragraph text -> $"{escapeSpecialChars text}\n\n"
        | Element.CodeBlock text -> $"```\n{escapeSpecialChars text}\n```\n\n"
        | Element.Table table ->
            let colLen =
                table 
                |> List.head
                |> List.length
            let colNamesRowSeparator =
                table
                |> List.head
                |> List.map (fun _ -> " --- |")
                |> String.concat ""
            let encodeRow row =
                row
                |> List.map (fun cell -> $" {escapeSpecialChars cell} |")
                |> String.concat ""
            let encodeAllRows =
                table
                |> List.skip 1
                |> List.map (fun row -> $"|{encodeRow row}\n")
                |> String.concat ""
            $"|{encodeRow (List.head table)}\n|{colNamesRowSeparator}\n{encodeAllRows}\n"
        | Element.OrderedList orderedList ->
            let encodeItems =
                orderedList
                |> List.map (fun item -> $"1. {escapeSpecialChars item}\n")
                |> String.concat ""
            $"{encodeItems}\n\n"
        | Element.UnorderedList unorderedList ->
            let encodeItems =
                unorderedList
                |> List.map (fun item -> $"* {escapeSpecialChars item}\n")
                |> String.concat ""
            $"{encodeItems}\n\n"
