Intractable Dialogue System (replace with name of project)

# Intro

This is an intractable dialogue system which will allow interactions with in-game NPCs designed for VR.

Users will be able to create custom Dialogue trees and traverse said trees using speech.

The NPC will appear to be immersive to the users who strikes a conversation.

# Requirements

- At least Unity 2018.4
- Basic knowledge and terminology of Unity.
- Windows 10
- Oculus Rift (preferred)
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

To open the Tree Editor window, in the top menu bar, click `Window`, and click the `customGUI` option.

This is the Tree Editor where we create trees, nodes, and modify various responses.

The left side of the window shows the Dialogue Trees for the project. To create or add a tree, press the `Add` button.

Once created a new tree will appear within the Tree Editor with an empty NPC prompt. You can click the tree within the list to focus on it.

A grey node is a NPC prompt, this is what the NPC will say to the user and play the given animation (if present).

There is a `New Child` button to add a user response depicted by the white nodes. If this response is matched by the verbal user input and the language engine, the system will traverse this branch.

![](./MDPics/treecreategui.gif)

## Step 6

Finally, with our newly created Dialogue Tree, we need to add the root node to the `TreeUI` game object.

First find the Dialogue node, so in the Project panel, navigate to `Assets\Resources\DialogueTree\TreeX` (replace X with your tree number) and click the folder.

In the Hierarchy, expand the `PatientSystem` game object, and open the `TreeUI` game object in the Inspector.

Drag the `Dialogue` (no numbered node represents root) node from the Project Panel over to the `Current Node` field in the Inspector.

![](./MDPics/addnodetoprefab.gif)









![](./MDPics/sttsettingerror.gif)
