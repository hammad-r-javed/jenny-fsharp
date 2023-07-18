module Jenny.Tests

open NUnit.Framework

// Table - Creation
[<Test>]
let ``Test table creation with empty column names list ``() =
    let tableCols = []
    let tableRows = [["data"]]
    match Jenny.Element.table tableCols tableRows with
    | Ok value -> Assert.That(value, Is.EqualTo(Jenny.Err.EmptyList))
    | Error value -> Assert.That(value, Is.EqualTo(Jenny.Err.EmptyList))

[<Test>]
let ``Test table creation with empty rows list``() =
    let tableCols = ["col 0"; "col 1"]
    let tableRows = []
    match Jenny.Element.table tableCols tableRows with
    | Ok value -> Assert.That(value, Is.EqualTo(Jenny.Err.EmptyList))
    | Error value -> Assert.That(value, Is.EqualTo(Jenny.Err.EmptyList))

[<Test>]
let ``Test table creation using rows with different number of columns each``() =
    let tableCols = ["col 0"; "col 1"]
    let tableRows = [["data"]]
    match Jenny.Element.table tableCols tableRows with
    | Ok value -> Assert.That(value, Is.EqualTo(Jenny.Err.RowsNotSameLengths))
    | Error value -> Assert.That(value, Is.EqualTo(Jenny.Err.RowsNotSameLengths))

[<Test>]
let ``Test table creation using rows with same number of columns each``() =
    let tableCols = ["col 0"; "col 1"]
    let tableRows = [["data0"; "data1"]]
    let expected = Jenny.Element.Table <| tableCols :: tableRows 
    match Jenny.Element.table tableCols tableRows with
    | Ok value -> Assert.That(value, Is.EqualTo(expected))
    | Error value -> Assert.That(value, Is.EqualTo(expected))

// Encoding Markdown
[<Test>]
let ``Test encoding heading 1, with all escapable chars, into markdown heading1``() = 
    let elem = Jenny.Element.heading1 "\\t`h*i_s{ }i[s] (j)ust # s+o-m.e rand!om text"
    Assert.That(Jenny.Markdown.encode elem, Is.EqualTo("# \\\\t\\`h\\*i\\_s\\{ \\}i\\[s\\] \\(j\\)ust \\# s\\+o\\-m\\.e rand\\!om text\n\n"))

[<Test>]
let ``Test encoding table with escapable chars into markdown table``() =
    let colNames = ["S!yntax"; "Descri`p`tion"]
    let rows = [
        ["Head__er"; "Ti)tle"];
        ["Para.graph"; "Tex-t"]
    ]
    let elem = Jenny.Table <| colNames :: rows
    Assert.That(Jenny.Markdown.encode elem, Is.EqualTo("| S\\!yntax | Descri\\`p\\`tion |\n| --- | --- |\n| Head\\_\\_er | Ti\\)tle |\n| Para\\.graph | Tex\\-t |\n\n"))


// Encoding HTML
// TODO
