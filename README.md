# Fit

*version 0.2*

Fit is a free CLI (Command Line Interface) application developed in C# designed to log personal metrics such as weight and height, with plans to include additional data pertaining to physical and mental health and well-being in the future.

Future updates will introduce features for generating charts from logged data, exporting functionalities, and more.

## Usage

### Initializing Fit

To start using Fit, execute the command `fit init`.

**Syntax:** 

    fit init <date-of-birth> <sex> <height> <weight>

**Example:**

    fit init 1981.05.27 male 175cm 108.6kg

Executing this command creates a Fit repository in the current directory and logs the initial values. For convenience, you may execute this command in the highest-level directory you prefer. Fit commands will operate in all subdirectories within this hierarchy.

**Currently, only the metric system is supported, and you must specify the unit with every value, without any spaces.**

### Logging Weight and Height

To log changes in your weight or height (noting that height changes are less frequent, but they do occur), you can use the `fit weight` and `fit height` commands.

**Syntax:**

     fit weight <weight>
     fit height <height>

**Examples:** 

     fit weight 107.6kg
     fit height 175.5cm

### Undo

If an error is made, you can revert the most recent entry with the `fit undo` command. This command will remove the last line from the fit.log file.

**Syntax and Example:**

     fit undo