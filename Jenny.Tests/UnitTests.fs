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
