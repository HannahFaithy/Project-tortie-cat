using UnityEngine;

// Declaration of the static class "MouseData"
public static class MouseData
{
    // Static variable to store the user interface that the mouse is currently hovering over
    public static UserInterface interfaceMouseIsOver;

    // Static variable to store the temporary reference to an item being dragged
    public static GameObject tempItemBeingDragged;

    // Static variable to track the specific slot being hovered over by the mouse
    public static GameObject slotHoveredOver;
}
