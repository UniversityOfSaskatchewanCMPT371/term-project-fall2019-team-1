# Incremental Deliverable 2

## Overview:

All artifacts related to ID2 can be found in the following folder: https://drive.google.com/open?id=1R71pIVSP-kJQNpI3SlJSIXdcajwRuf9e
For all group organization information, please see the Group Policy Document here:
https://drive.google.com/open?id=11sec8O1YRpXrZ_SmS7pG_yDxbLzbfYKHDlcEEOF8y9s

## Mini-Milestones:

For this deliverable, our milestones were as follows:
* Complete Design Document - Completed
* Implement Debugger UI - Completed
* Implement Interaction Logging System - Completed
* Integration of Speech-to-Text and Text-to-Speech with Dialogue Tree - Completed
* Demo for Shareholder - Incomplete

For the next deliverable, our milestones are as follows:
* Import/Export Feature Implementation
* First Version of Language Processing Engine Implemented
* Migration of Windows DLL Dependencies to Python Libraries
* Implement Slack CI Integration
* CI Implementation for All dev/master/release Commits

## Running the Project:

Before attempting to run the project, ensure that if you are using Windows 10 that you go into Settings>Privacy>Speech and enable Online Speech Recognition. If this option is not enabled, the dialogue tree cannot be traversed and the demo will not work.
The project can be run by downloading the release, unzipping the "Test_Build_Unzip_Me.Zip" file, and pressing "VRPatientInteraction.exe"
There are 3 dialogue paths within the "VRPatientInteraction.exe" test scene. The expected responses from the user are given in quotes below, where the NPC prompts are in square brackets:
1. [Hello] > "Does your stomach hurt?" > [Yes it does]
2. [Hello] > "Hello" > [Goodbye] > "Goodbye" > [Thank you for grading the project]
3. [Hello] > "What is the average airspeed velocity of an unladen swallow?" > [African or European?]

## Creating a Dialogue Tree:

In Resources/DialogueTree/Tree1, right-click and select Create>ScriptableObject>Dialogue to create a new Dialogue Tree Node
## In the Inspector window: 
The Prompt field is the text that you want the NPC to vocalize to the Scene User.
The Response field will allow you to specify how many branches come off of the current node, as well as what keywords trigger each of the branches.
The Next field will allow you to drag and drop new nodes into each of the corresponding branch locations.
=======
# term-project-fall2019-team-1
## [ID1 Release](https://github.com/UniversityOfSaskatchewanCMPT371/term-project-fall2019-team-1/releases/tag/ID1)
## [ID2 Release](https://github.com/UniversityOfSaskatchewanCMPT371/term-project-fall2019-team-1/releases/tag/ID2.1.1)

