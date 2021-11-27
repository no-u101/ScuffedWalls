# Custom Functions & Workspaces

Hate writing kabillions of lines for the same effects? ME TOO! ScuffedWalls offers a solution for this...

Custom Functions!!!

## Contents

table of the contents
 - [`Abstract`](#abstract)
 - [`Basic Syntax`](#basic-syntax)
 - [`Argument Syntax`](#argument-syntax)

## Abstract

Custom Functions (CFs) are essentially just a `workspace` can be instanced multiple times with custom arguments.

Example: The Cube Generator (iswimfly) is a ~200 line long SW script that creates a cube made of 12 walls. If we wanted to create one cube, simply paste the cube script into our own script. But what if we wanted to create more than one? Normally you would have to paste the cube script twice! (400 lines of code!) 3 cubes? (600 lines!!!!) 10 cubes? (2000 lines!!!!) BUT NOW - we can simply define the cube script as a Custom Function. This allows us to call this custom function as many times as we would like. For all the small values that change, like (for example) size, position, rotation, we can pass those in as arguments in our custom function.

## Basic Syntax

**-> Note**
 - ScuffedWalls is NOT case sensitive.

Both Workspaces and Custom Functions have the same defining syntax. `function:Name` or `workspace:Name`

**-> Note**
 - Workspaces do not need a name
 - Workspaces are containers for map objects, not functions. functions can add/modify map objects in the workspace.
 - (suggestion) Workspaces should be used in places where functions like `Append` are used.
 - Everything in all of your workspaces (walls, notes, events, etc) will be combined when exporting to the map file.

```ruby
Workspace

# Things stay here (walls, notes, events, etc)
# Things are added in and changed by functions called from in here!

Workspace: Workspace Name!!!!

# Things also go in here
```

```ruby
Function: Custom Function Name!!!

# Things are added in and changed by functions called from in here!
# Things that are in here will go into the workspace from where this is called from (walls, notes, events, etc)
```

To call a CF, simply reference it as if it was a regular ScuffedWalls function.

**-> Note**
 - `0` is the calltime, the time at which the function is called, this can be any number. This can mean a variety of things depending on how it is used.
 - `CustomFunction` is the name of a CF created somewhere. They can be defined and called from anywhere.

```ruby
0: CustomFunction
```

Functions called from inside a CF can happen at any time, or at the calltime of the CF. This can be done by substituting in `XXX` for the number.

```ruby
function: AddAWall

13.5:Wall

XXX:Wall
```

**Full Example**
```ruby
Function: AddAWall

13.5:Wall

XXX:Wall

Workspace

7.25:addAWall
```

## Argument Syntax

If you have not read about [`Variables`](https://github.com/thelightdesigner/ScuffedWalls/blob/2.0-docs/Functions.md#Variables) in SW, I suggest doing so. Custom parameters for CFs use variables as a means of defining what data can and cannot be passed into a CF.

Variables can be set as public, meaning that they are accessable from the call site.

```ruby
Function: AddAWall

var:HowManyWallsToAdd
  data:1
  public:true
```

To reference a public variable in a CF call, list it beneath as shown.

```ruby
Workspace

0:AddAWall
  howManyWallsToAdd:5
```

You can set this variable to different values in different CF calls.

```ruby
Workspace

0:AddAWall
  howManyWallsToAdd:5

0:AddAWall
  howManyWallsToAdd:87

0:AddAWall
  howManyWallsToAdd:2232
```

Essentially, this is a way of passing in arguments to custom functions.

```ruby
Function: PrintWhatISay

var:SayThis
  data:
  public:true

0:print
  log:SayThis

Workspace

0:PrintWhatISay
  SayThis: hello!!!

0:PrintWhatISay
  SayThis: hi!!!

0:PrintWhatISay
  SayThis: bye :(((
```

**-> Note**
  - Variable names ARE case sensitive
  - all functions (Regular and custom) have these parameters: `repeat`, `repeataddtime`, `funtime`, `log`


