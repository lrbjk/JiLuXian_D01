Blood works on mobile / PC / consoles with vertexlit / forward / deferred renderer and dx9, dx11, openGL.

Effect of blood has offset pivot point. This makes it easy inctantiate blood at the point of impact 
and set the direction using normal (see RaycastHit.point and RaycastHit.normal).

Blood uses red uv-textures. You can change the transparency/red intensity of the blood by changing the color of the material.
If you want to change the color of the blood (such as green), use materials and prefab "coloredBlood" with grayScale textures.