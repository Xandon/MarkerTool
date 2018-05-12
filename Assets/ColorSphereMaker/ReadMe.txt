Color Sphere Maker

Assets are contained in ColorSphereMaker/Resources.

Add the ObjectPlacementNode to the hand gameobject that maps to one of the controllers. Once added, lcick on it and make sure nothing looks broken in the imspector. There should be 2 arrays, one for the sphere prefabs in each color, and another for the colors themselves (I used those in the UI on a piece of text that shows the current position in the selected color).

Check Edit > Project Settings > Input. There should be 8 axes at the bottom (it says Axes but these are actually buttons) - 14, 15, 8, 9, 16, 17, 18, 19. There's two of each button, one for each controller so you should be able to use either controller.

Functions:
14/15 - trigger, for placing spheres
8/9 - thumbstick button press, for changing color
16/17/18/19 - touchpad touch/press for adjusting Z position of sphere placement (note the white translucent sphere where the controller is - this will shift back and forth with these controls)

Note - the button numbers are exposed in the editor on the ObjectPlacementNode if you need to change things around to use different buttons.