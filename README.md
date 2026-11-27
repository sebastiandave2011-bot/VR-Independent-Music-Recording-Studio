VR Independent Music Recording Studio

An interactive Virtual Reality experience developed in Unity 6 for standalone Meta Quest headsets. The project recreates an independent music production and recording studio where users can explore the space, interact with studio equipment, and complete a 6-interaction challenge within a 120-second timer using World Space UI.

Author: Nahum Sebastian Bermeo Barrera
Course: Virtual Reality (213043) – Universidad Nacional Abierta y a Distancia (UNAD)

Technologies Used

Game Engine: Unity 6 (6000.5.10f1 / Unity 6.3 LTS workflow) with Universal Render Pipeline (URP)

XR Frameworks: XR Interaction Toolkit, OpenXR Plugin, Meta Quest SDK, and XR Device Simulator

Programming Language: C# (documented with XML summary comments)

3D Modeling & Optimization: Blender (mesh decimation and retopology)

Target Platform: Android (ARM64 architecture, IL2CPP scripting backend, Android SDK API 35)

How to Build and Install the APK

Option A: Direct Installation on Meta Quest (Pre-compiled APK)

Download the compiled EstudioVR_Fase4.apk (~203 MB) from the release/Drive link provided in the project documentation.

Enable Developer Mode on your Meta Quest headset via the Meta Horizon mobile app.

Connect the headset to your PC via USB-C and authorize USB debugging inside the headset.

Install the .apk file using SideQuest or Meta Quest Developer Hub (MQDH).

Launch the app from App Library > Unknown Sources inside your Meta Quest.

Option B: Building from Source in Unity

Clone or download this repository and open the root folder in Unity Hub using Unity 6 with the Android Build Support module (OpenJDK, Android SDK, and NDK) installed.

Open the main scene located at Assets/Scenes/SampleScene.unity.

Go to File > Build Profiles (or Build Settings) and switch the active platform to Android.

Ensure Texture Compression is set to ASTC, Scripting Backend is set to IL2CPP, and Target Architectures has ARM64 checked.

Click Build (or Build And Run with the headset connected) to generate the signed .apk package.

Known Issues and Future Improvements

Known Issues

World Scale & Locomotion Calibration: Imported architectural assets required a scale factor adjustment, which initially made continuous movement feel slow until locomotion speed and footstep distance thresholds were recalibrated.

Baked Lighting Artifacts: Re-importing decimated meshes required clearing legacy baked lightmaps (Clear Baked Data) to avoid shadow blotches on walls; real-time dynamic lights are currently prioritized for the main switch and LED tubes.

Future Improvements

Interactive Audio Mixing Console: Add functional faders and knobs on the studio desk to mix individual stems (guitars, drums, vocals) in real time.

Spatial HRTF Acoustics: Implement dynamic reverb zones and binaural audio filters when moving between the recording booth area and the control desk.

Multi-Room Expansion: Connect the main studio door to an isolated vocal booth and lounge area.

AI Tools Used (Declaration Summary)

Generative AI tools were used as technical assistants during conceptual design, C# scripting, and optimization workflows, with all outputs manually validated and integrated in Unity:

Google Gemini: Assisted with architectural reference sketches, VR accessibility evaluation (selecting continuous locomotion + blink teleportation to prevent motion sickness), editor optimization scripting (VRStudioOptimizer.cs), and technical documentation.

Cursor & DeepSeek: Used for AI-assisted C# scripting (vibecoding) of coroutines, emissive material toggles, and resolving runtime NullReferenceException issues in GameStateManager.cs.

Tripo AI: Used to generate base 3D studio props, which were subsequently decimated and optimized in Blender to reduce scene geometry from 70M triangles down to ~171k–442k triangles for smooth 72+ FPS performance on Meta Quest.
