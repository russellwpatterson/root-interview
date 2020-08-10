# Root Code Sample

## Overview

First of all, thanks for taking a look at this code sample. I'm sure you get a ton of these and I appreciate you taking the time to check mine out.

This was an interesting code challenge. It's small enough to where I didn't want to over engineer it, but obviously you need to see some level of design and thoughtfulness throughout it, so I hope I was able to find the right balance.

I chose to implement my solution using .NET Core 3.1 on macOS. I don't think there's any reason why it wouldn't run on Windows or Linux, but I've only tested it on macOS. I've been doing C# web development since 2009, so I'm strongest with the .NET platform.

The unit tests for this project have been created using Xunit, which is one of the most popular .NET testing frameworks as of now. I wrote unit tests for all of the "happy path" and error conditions that I could think of.

I've made an effort to do as much of a TDD approach as possible, but there is some console code (in Program.cs) that doesn't have unit tests, as it's mostly dependent on piping on the console or loading the provided filename when the program is executed. I did test both of these scenarios using bash scripts to ensure that they worked. If you want to run those, just make sure you run "build-mac.sh" first to build the executable it uses.

## Thought Process

I approached this problem with a couple different things in mind. I wanted to ensure that the Single Responsibility Principle was used as much as possible. In service of that goal, I created a class for validating each line of the data file, and a class for parsing each type of line as well. I used the idea of Inversion of Control and Liskov's Substitution Principle to reduce the coupling between the parser and the validator implementations, by using constructor injection to pass in the validator to the parser and only depending on the underlying interface.

Across the codebase, I coded to the interface wherever possible, and used the most-accepting base interface that I could, in order to leave things as open as I could. An example of this is that in many places, I chose to use `IEnumerable<T>` instead of `List<T>`. Using `IEnumerable` means that not only can I pass in a `List`, but I can also pass in an array or other data structure as long as it implements `IEnumerable`. By doing this, I have more flexibility when I actually use the method and can save time on conversions or shaping of the data.

Below, I go into some specifics about all of the high level components.

### Validators ###

To validate each line of the data file, I created an implementation of the `IValidator` interface that defines specific rules for each type of command that is possible. I've chosen to throw an exception instead of returning a boolean, as I believe that for this application, an invalid line is a case where the program cannot continue and is "exceptional". If the line passes validation, I made a choice to pass the parts of the line as an out parameter so that I would not have to waste the cycles later splitting the line again.

### Parsers ###

For each type of line, I've created an implementation of the generic interface `IParser<T>`. It is the responsibility of this class to parse a single line of the provided file and return a `T` (either `Driver` or `Trip` for this program) containing the data from the line, having been validated and parsed. On construction, an `IValidator` is passed and then used as the first step of parsing the file. There are a number of exceptions thrown if the line is invalid. Once validation is passed, the data is valid, and an instance of the appropriate model class is created using the parsed and validated data.

### Factories ###

I created a factory for the parsers and validators to demonstrate how I might approach that. For the parsers, I use C#'s generic methods and pattern matching and get the appropriate `IParser<T>` implementation based on the type parameter. The approach for the validators is similar but slighty different as the `IValidator` interface is not a generic interface, which simplifies things a bit. In a production application, I might replace this with a Dependency Injection container to further reduce the coupling between the various components, but I chose not to in order to keep the project as simple as possible.

### Parsing Service ###

To wrap together the whole process, I created a class called `ParsingService` that loops through each line of the file, and uses the appropriate `IParser<T>` to gather the provided data. Once the data has been completely parsed, the `ParsingService` creates a `DriverSummary` object for each driver. This `DriverSummary` contains all of the data that would be printed out, and an overridden `ToString()` method that formats the data as requested by the specification.

### Other Pieces ###

There are a few other pieces that I opted to use. One of them is an extension method on a `List<Driver>` that gets a `Driver` object by DriverName. I used this for convenience and readability, as it's way easier in my opinion to read `drivers.GetDriver(name)` than `drivers.FirstOrDefault((o) => o.DriverName == name)` every time I need to get a driver by their name.

Another choice I made was to create custom `Exception` classes to signify what I believed to be the most critical errors that could happen during execution. These include a situation when something other than "Driver" or "Trip" is provided (`InvalidDataLineTypeException`), a malformed data line (`InvalidDataLineException`) and finally, trip data that has been attributed to a driver that was not previously defined (`UndeclaredDriverException`).

## Closing ##

Again, I'd like to thank you for taking a look at this code, and I'd be happy to address any questions or concerns if I'm fortunate enough to move forward in the interview process.

