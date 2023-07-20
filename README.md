# Jenny F# - WIP
My personal small F# lib to help with generating Markdown/HTML reports.

I'm getting familiar with F# and so The project is a WIP and the direction for the project hasn't been finalised yet. The goal of the lib at the moment is to provide a typed way to format text into Markdown/HTML elements, which can then be used to generate reports.

# Installation
[Nuget Page for HRJDev.Jenny](https://www.nuget.org/packages/HRJDev.Jenny)

# Usage
"Elements" can be created and then "encoded" into HTML or Markdown formatted strings. When encoding to either HTML or Markdown, special chars will be escaped.

E.g, to convert `"My Heading 3"` to markdown `"### My Heading 3"`, you would run the following:

```fsharp
"My Heading 3"
|> Jenny.Element.heading3
|> Jenny.Markdown.encode
```

List of element constructors + type signatures
```fsharp
Jenny.Element.heading1          // string -> Element
Jenny.Element.heading2          // string -> Element
Jenny.Element.heading3          // string -> Element
Jenny.Element.heading4          // string -> Element
Jenny.Element.heading5          // string -> Element
Jenny.Element.heading6          // string -> Element
Jenny.Element.paragraph         // string -> Element
Jenny.Element.codeBlock         // string -> Element
Jenny.Element.table             // list<string> -> list<list<string>> -> Result<Element, Err>
Jenny.Element.orderedList       // list<string> -> Result<Element, Err>
Jenny.Element.unorderedList     // list<string> -> Result<Element, Err>
```

Err Type:
```fsharp
type Err =
    | RowsNotSameLengths
    | EmptyList
```
No list that is passed to either table, ordered list, unordered list constructors is allowed to be empty, otherwise `Jenny.Err.EmptyList` will be returned inside the Result. Number of columns in each row (length of the lists) all must be the same as well. 

Example of encoding a created element:
```fsharp
let heading2 = Jenny.Element.heading2 "Some Title"

// Encode to Markdown
Jenny.Markdown.encode heading2 // will return "## Some Title"

// Encode to HTML (not implemented yet)
Jenny.HTML.encode heading2
```
