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

    let private escapeString text =
        text
        |> String.collect escapeChar

    let encode element =
        match element with
        | Element.Heading1 text -> $"# {escapeString text}\n\n"
        | Element.Heading2 text -> $"## {escapeString text}\n\n"
        | Element.Heading3 text -> $"### {escapeString text}\n\n"
        | Element.Heading4 text -> $"#### {escapeString text}\n\n"
        | Element.Heading5 text -> $"##### {escapeString text}\n\n"
        | Element.Heading6 text -> $"###### {escapeString text}\n\n"
        | Element.Paragraph text -> $"{escapeString text}\n\n"
        | Element.CodeBlock text -> $"```\n${escapeString text}\n```\n\n"
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
                |> List.map (fun cell -> $" {escapeString cell} |")
                |> String.concat ""
            let encodeAllRows =
                table
                |> List.skip 1
                |> List.map (fun row -> $"|{encodeRow row}\n")
                |> String.concat ""
            $"|{encodeRow (List.head table)}\n|{colNamesRowSeparator}\n{encodeAllRows}\n"

        | _ -> "TODO"
