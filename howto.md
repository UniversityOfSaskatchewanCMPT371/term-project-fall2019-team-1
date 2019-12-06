Intractable Dialogue System (replace with name of project)

# Intro

This is an intractable dialogue system which will allow interactions with in-game NPCs designed for VR (VRTK).

Users will be able to create custom Dialogue trees and traverse said trees using speech.

The NPC will appear to be immersive to the users who strikes a conversation.

# Requirements

- At least Unity 2018.4
- Basic knowledge and terminology of Unity.
- Windows 10
- Oculus Rift (preferred, for VR)
- VRTK (for VR, [setup](https://github.com/ExtendRealityLtd/VRTK.Prefabs), this project and SampleScene has everything setup already)
- An on-going internet connection
- Audio input device (microphone)
- Audio output device (speakers)

# Setup

## Step 1

(get user to receive the assets required for the system, and install it)

## Step 2

First open the desired scene to insert the system into.
In this case, it will be blank scene.

## Step 3

Within the opened scene, navigate within the Project panel to
`Assets\PatientSystem\Prefabs`, click the folder and drag the prefab into the Hierarchy.

![](./MDPics/prefabdrag.gif)

## Step 4 (for animating, optional)

For this step, you'll need a game object that has a `Animator` component. This is the object to play animations on.

So within the Hierarchy, expand the `PatientSystem` object and open the `NPC` game object in the Inspector.

Drag the game object with the `Animator` component onto the `Animator` field in the Inspector.

(for now, AudioSource is not implemented)

![](./MDPics/npcprefablink.gif)

## Step 5

To open the Tree Editor window, in the top menu bar, click `Window`, and click the `DialogueBuilder` option.

This is the Tree Editor where we create trees, nodes, and modify various responses.

The left side of the window shows the Dialogue Trees for the project. To create or add a tree, press the `Add` button. You can also rename trees with the text field.

Once created a new tree will appear within the Tree Editor with an empty NPC prompt. You can click the tree within the list to focus on it.

A grey node is a NPC prompt, this is what the NPC will say to the user and play the given animation (if present).

There is a `New Child` button to add a user response depicted by the white nodes. If this response is matched by the verbal user input and the language engine, the system will traverse this branch.

![](./MDPics/treecreategui.gif)

## Step 6

Finally, with our newly created Dialogue Tree, we need to add the tree name to the `TreeUI` game object.

Find out what your desired tree name in the DialogueBuilder interface.

In the Hierarchy, expand the `PatientSystem` game object, and open the `TreeUI` game object in the Inspector.

Add your tree name to the `Current Tree` field in the Inspector.

![](./MDPics/addnodetoprefab.gif)

## Step 7

The system is good to go, once the scene is ran, the root Dialogue node's prompt will be said by the NPC.

The user can then give Audio Input to the system and if language engine detects a match, it traverse to that branch.

If no match found, the NPC will repeat the previous prompt.

Once the tree had hit a leaf node, the NPC will indicate the end of the conversation and the NPC will stop listening for input.

# Common Errors

(add more errors)

## 'Dictation Support is not enabled on this Device'

We need to enable the `Online Speech Recognition` option in Windows.

So open Windows Settings, open the `Privacy` menu, and the `Speech` menu, and enable the `Online Speech Recognition` option.

![](./MDPics/sttsettingerror.gif)

# Known bugs

(Fill it in)

# Credits

(needs better formatting)

Scene/project uses `https://github.com/ExtendRealityLtd/VRTK`

(find speechlib, SpVoice object)

(insert roles and contributions)
