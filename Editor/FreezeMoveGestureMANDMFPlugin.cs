using System;

using System.Collections.Generic;
using System.IO;
using nadena.dev.modular_avatar.core;
using nadena.dev.modular_avatar.core.menu;
using nadena.dev.ndmf;
using sweetforest.freezemovegesturema.Components;
using sweetforest.freezemovegesturema.Editor;
using UnityEditor.Animations;
using UnityEngine;


[assembly: ExportsPlugin(typeof(FreezeMoveGestureMANDMFPlugin))]

namespace sweetforest.freezemovegesturema.Editor
{
    public class FreezeMoveGestureMANDMFPlugin : Plugin<FreezeMoveGestureMANDMFPlugin>
    {
        //  private const string ParameterNameForEnable = "FreezeMoveGesture/Enable";
        public override string DisplayName => "Freeze Move Gesture MA";
        protected override void Configure()
        {

            InPhase(BuildPhase.Generating)
                .BeforePlugin("nadena.dev.modular-avatar")
                .Run("Freeze Move Gesture MA", ctx =>
                {

                    try
                    {
                        Build(ctx);
                        Debug.Log("Freeze Move Gesture MA: done!");
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error: " + e);
                    }

                })
                ;

        }

        public void Build(BuildContext ctx)
        {
            var freezeMoveGestureComponent = ctx.AvatarRootObject.GetComponent<FreezeMoveGestureComponent>();

            if (freezeMoveGestureComponent == null) return;

            var gestureDictionary = new Dictionary<string, bool>
    {
        { "LeftFist", freezeMoveGestureComponent.LeftFist },
        { "RightFist", freezeMoveGestureComponent.RightFist },
        { "LeftOpen", freezeMoveGestureComponent.LeftOpen },
        { "RightOpen", freezeMoveGestureComponent.RightOpen },
        { "LeftPoint", freezeMoveGestureComponent.LeftPoint },
        { "RightPoint", freezeMoveGestureComponent.RightPoint },
        { "LeftPeace", freezeMoveGestureComponent.LeftPeace },
        { "RightPeace", freezeMoveGestureComponent.RightPeace },
        { "LeftRockNRoll", freezeMoveGestureComponent.LeftRockNRoll },
        { "RightRockNRoll", freezeMoveGestureComponent.RightRockNRoll },
        { "LeftGun", freezeMoveGestureComponent.LeftGun },
        { "RightGun", freezeMoveGestureComponent.RightGun },
        { "LeftThumbsUp", freezeMoveGestureComponent.LeftThumbsUp },
        { "RightThumbsUp", freezeMoveGestureComponent.RightThumbsUp }
    };

            // Get the path to the assembly where this script is located
            string assetsPath = Application.dataPath;

            string controllerPath = Path.Combine(assetsPath, "../Packages/sweetforest.freezemovegesturema/Animations/ControllerTemplate.controller.txt");


         // Refresh Asset Database
            UnityEditor.AssetDatabase.Refresh();

            if (!File.Exists(controllerPath))
            {
                Debug.LogError("File not found controller template: " + controllerPath);
                return;
            }
            else
            {
                Debug.Log("Controller Path: " + controllerPath + " found");
            }
            // Load text from path
            string controllerText = File.ReadAllText(controllerPath);

            // Replace placeholders in the controller text
            foreach (var item in gestureDictionary)
            {
                var key = item.Key;
                var value = item.Value;

                // Replace "%Key%" with "0" or "1"
                controllerText = controllerText.Replace($"%{key}%", value ? "0" : "1");
            }
            controllerText = controllerText.Replace("%ControllerTemplate%", "generated");
            // save file to /Assets/_FreezeMoveGestureMA/generated.controller

            string generatedControllerPath = Path.Combine(Application.dataPath, "../Assets/Sweet Forest Generated/_FreezeMoveGestureMA/generated.controller");
            var folderPath = Path.Combine(Application.dataPath, "../Assets/Sweet Forest Generated/_FreezeMoveGestureMA");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log($"Created folder Sweet Forest Generated/_FreezeMoveGestureMA");
            }
            if (File.Exists(generatedControllerPath))
            {
                File.Delete(generatedControllerPath);
                Debug.Log($"Deleted existing controller file: {generatedControllerPath}");
            }
            File.WriteAllText(generatedControllerPath, controllerText);

            // Refresh Asset Database
            UnityEditor.AssetDatabase.Refresh();

            // after that load to Animator Controller
            var generatedAnimator = UnityEditor.AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Sweet Forest Generated/_FreezeMoveGestureMA/generated.controller");

            if (generatedAnimator == null)
            {
                Debug.LogError("Failed to load Animator Controller at path: " + generatedControllerPath);
                return;
            }
            GameObject gameObject = new GameObject("Merge Animator");
            gameObject.transform.parent = ctx.AvatarRootObject.transform;
            var r = gameObject.AddComponent<ModularAvatarMergeAnimator>();
            r.layerType = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor.AnimLayerType.Gesture;
            r.animator = generatedAnimator;

            
            var parameterComponent = gameObject.AddComponent<ModularAvatarParameters>();

            var parameterConfig = new ParameterConfig();
            parameterConfig.defaultValue = 0;
            parameterConfig.saved = true;
            parameterConfig.localOnly = true;
            parameterConfig.syncType = ParameterSyncType.Bool;
            parameterConfig.nameOrPrefix = "FreezeMoveGesture/Enable";
            parameterComponent.parameters.Add(parameterConfig);

            if(freezeMoveGestureComponent.AddToggleMenu) {
                gameObject.AddComponent<ModularAvatarMenuInstaller>();
                var menuItem = gameObject.AddComponent<ModularAvatarMenuItem>();
                menuItem.name = "Freeze Move Gesture";
                menuItem.Control.name = "Freeze Move Gesture";


                menuItem.Control.type = VirtualControl.ControlType.Toggle;
                var vControlParameter = new VirtualControl.Parameter();

                vControlParameter.name = "FreezeMoveGesture/Enable";

                menuItem.Control.parameter = vControlParameter;
            }
        }


    }

}
