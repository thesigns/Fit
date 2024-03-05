# Fit

*version 0.4.1*

Fit is a free CLI (Command Line Interface) application developed in C# designed to store personal metrics such as weight, height and emotions, with plans to include additional data pertaining to physical and mental health and well-being in the future.

Future updates will introduce features for generating charts from stored data, exporting functionalities, and more.

## Usage

### Initializing Fit

To start using Fit, execute the command `fit init`.

**Syntax:** 

    fit init <date-of-birth> <sex> <height> <weight>

**Example:**

    fit init 1981.05.27 male 175cm 108.6kg

Executing this command creates a Fit repository in the current directory and stores the initial values. For convenience, you may execute this command in the highest-level directory you prefer. Fit commands will operate in all subdirectories within this hierarchy.

**Currently, only the metric system is supported, and you must specify the unit with every value, without any spaces.**

### Storing Weight and Height

To store changes in your weight or height (noting that height changes are less frequent, but they do occur), you can use the `fit weight` and `fit height` commands.

**Syntax:**

     fit weight <weight>
     fit height <height>

**Examples:** 

     fit weight 107.6kg
     fit height 175.5cm

### Storing Mood

To store changes in your emotional state, you can use the `fit mood` command. Emotions are stored using traditional western emoticons. Allowed emoticons:

    :((( - Extremely Sad
    :((  - Very Sad
    :(   - Sad
    :I   - Indifferent
    :)   - Happy
    :))  - Very Happy
    :))) - Extremely Happy
    >:[  - Angry
    :'(  - Crying
    X(   - Tired   
    :<   - Disappointed
    :S   - Confused
    :O   - Surprised
    :P   - Playful
    :*   - Affectionate
    <3   - In Love

**Syntax:**

     fit mood <emoticon>

**Examples:**

     fit mood :P

### Undo

If an error is made, you can revert the most recent log entry with the `fit undo` command. This command will remove the last line from the fit.log file.

**Syntax and Example:**

     fit undo
