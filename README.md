# Fit

*Version 0.9.1*

*Note: This is a very "work in progress" personal project. Version 0.9 introduces some changes in log.fit format which make the new version incompatible with repositories saved by the previous version. It is possible to fix the file manually. Sorry!*

Fit is a free CLI (Command Line Interface) application developed in C#, designed to store physical and mental personal metrics such as weight, height, and emotional states, with plans to include additional features pertaining to health and well-being in the future.

Among other things, future updates will introduce features for generating reports from stored data, exporting functionalities, and more.

## Usage

### Initializing Fit

To start using Fit, execute the command `fit init`.

**Syntax:** 

    fit init <date-of-birth> <sex> <height> <weight>

**Example:**

    fit init 1981.05.27 male 175cm 108.6kg

Executing this command creates a Fit repository in the current directory and stores some initial values. For convenience, you may execute this command in the highest-level directory you prefer. Fit commands will operate in all subdirectories within this hierarchy.

**Both metric and imperial unit system is supported. You must specify the unit abbreviation with every value, without any spaces.**

### Storing Weight and Height

To store changes in your weight or height (noting that height changes are less frequent, but they do occur), you can use the `fit weight` and `fit height` commands.

**Syntax:**

     fit weight <weight>
     fit height <height>

**Examples:** 

     fit weight 107.6kg
     fit height 175.5cm

### Storing Emotions

To store changes in your emotional state, you can use the `fit emo` command. Emotions are stored using traditional western emoticons. Allowed emoticons:

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
    *)   - Focused
    @(   - Distracted   
    :S   - Confused
    :O   - Surprised
    ;P   - Playful
    :D   - Laughing
    :*   - Affectionate
    <3   - In Love
    :?   - Indescribable

Each emotion is assigned a value from -5 (worst) to 5 (best), which will be used in the future to rank emotional changes.

**Syntax:**

     fit emo <emoticon>

**Examples:**

     fit emo :P

### Undo

If an error is made, you can revert the most recent log entry with the `fit undo` command. This command will *comment* the last uncommented line in the fit.log file. It does not remove anything, just comments out.

**Syntax and Example:**

     fit undo
