## HtmlMakerKit

Set of tools that helps creating html elements dynamically in C# code with modifying its attributes and inner content (text and children).

## Installation  

Install the standard Nuget packages into your C# project:

[HtmlMakerKit](https://www.nuget.org/packages/HtmlMakerKit) for all set of tools.

[HtmlMakerKit.Core](https://www.nuget.org/packages/HtmlMakerKit.Core) for tools targeted on creating html elements in general.

### Compatibility

| Project | .NET version |
|--|--|
| HtmlMakerKit | 5.0 |
| HtmlMakerKit.Core | 5.0 |


## Usage

For details visit [Wiki](https://github.com/BartoszKopec/HtmlMakerKit/wiki/Gettings-started)

#### HtmlMakerKit.Core:
```c#
HtmlElement html = HtmlElement.New("html").AddChildren(
   HtmlElement.New("head").AddChildren(
      HtmlElement.New("title").SetInnerHTML("My website!"),
         HtmlElement.New("link").AddAttibutes(
            ("rel", "stylesheet"),
	    ("href", "http://example.org/style.css")
	 ),
	 HtmlElement.New("style").SetInnerHTML(
	    "body{color:black;} p{font-size:1.1em;} .title{color:blue;}"
	 )
      ),
      HtmlElement.New("body").AddChildren(
         HtmlElement.New("h1")
	    .AddAttibutes(("class", "title"))
	    .SetInnerHTML("Mega welcome on my website!"),
	 HtmlElement.New("p").SetInnerHTML("Lorem ipsum dotor amar"),
	 HtmlElement.New("script").SetInnerHTML(
            "function job(){console.log(\"bar\");"
	 )
      )
   );
```
will produce:
```html
<html>
   <head>
      <title>My website!</title>
      <link rel="stylesheet" href="http://example.org/style.css"/>
      <style>
	body{
	   color:black;
	} 
	p{
	   font-size:1.1em;
	} 
	.title{
	   color:blue;
	}			
      </style>
   </head>
   <body>
      <h1 class="title">Mega welcome on my website!</h1>
      <p>Lorem ipsum dotor amar</p>
      <script>
	function job(){console.log("bar");
      </script>
   </body>
</html>
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
Please make sure to update tests as appropriate.

## License

[MIT](https://github.com/BartoszKopec/HtmlMakerKit/blob/main/LICENSE)
