﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BNG {

    [CustomEditor(typeof(GrabPoint))]
    [CanEditMultipleObjects]
    public class GrabPointEditor : Editor {

        public GameObject LeftHandPreview;
        bool showingLeftHand = false;

        public GameObject RightHandPreview;
        bool showingRightHand = false;

        // Define a texture and GUIContent
        private Texture buttonLeftTexture;
        private Texture buttonLeftTextureSelected;
        private GUIContent buttonLeftContent;

        private Texture buttonRightTexture;
        private Texture buttonRightTextureSelected;

        private GUIContent buttonRightContent;

        GrabPoint grabPoint;

        SerializedProperty handPoseType;
        SerializedProperty SelectedHandPose;
        SerializedProperty HandPose;
        SerializedProperty LeftHandIsValid;
        SerializedProperty InvertLeftHand;
        SerializedProperty InvertLeftPositionX;
        SerializedProperty InvertLeftPositionY;
        SerializedProperty InvertLeftPositionZ;
        SerializedProperty InvertLeftEulerX;
        SerializedProperty InvertLeftEulerY;
        SerializedProperty InvertLeftEulerZ;

        SerializedProperty RightHandIsValid;
        SerializedProperty HandPosition;
        SerializedProperty MaxDegreeDifferenceAllowed;
        SerializedProperty IndexBlendMin;
        SerializedProperty IndexBlendMax;
        SerializedProperty ThumbBlendMin;
        SerializedProperty ThumbBlendMax;

        SerializedProperty LeftHandPreviewPrefab;
        SerializedProperty RightHandPreviewPrefab;

        SerializedProperty ShowAngleGizmo;

        // Default GameObject to use Resources.Load on
        static string _defaultRightModelPreviewName = "RightHandModelsEditorPreview";
        static string _defaultLeftModelPreviewName = "LeftHandModelsEditorPreview";

        void OnEnable() {
            handPoseType = serializedObject.FindProperty("handPoseType");
            SelectedHandPose = serializedObject.FindProperty("SelectedHandPose");
            HandPose = serializedObject.FindProperty("HandPose");
            LeftHandIsValid = serializedObject.FindProperty("LeftHandIsValid");
            InvertLeftHand = serializedObject.FindProperty("InvertLeftHand");

            InvertLeftPositionX = serializedObject.FindProperty("InvertLeftPositionX");
            InvertLeftPositionY = serializedObject.FindProperty("InvertLeftPositionY");
            InvertLeftPositionZ = serializedObject.FindProperty("InvertLeftPositionZ");

            InvertLeftEulerX = serializedObject.FindProperty("InvertLeftEulerX");
            InvertLeftEulerY = serializedObject.FindProperty("InvertLeftEulerY");
            InvertLeftEulerZ = serializedObject.FindProperty("InvertLeftEulerZ");

            RightHandIsValid = serializedObject.FindProperty("RightHandIsValid");
            HandPosition = serializedObject.FindProperty("HandPosition");
            MaxDegreeDifferenceAllowed = serializedObject.FindProperty("MaxDegreeDifferenceAllowed");
            IndexBlendMin = serializedObject.FindProperty("IndexBlendMin");
            IndexBlendMax = serializedObject.FindProperty("IndexBlendMax");
            ThumbBlendMin = serializedObject.FindProperty("ThumbBlendMin");
            ThumbBlendMax = serializedObject.FindProperty("ThumbBlendMax");

            LeftHandPreviewPrefab = serializedObject.FindProperty("LeftHandPreviewPrefab");
            RightHandPreviewPrefab = serializedObject.FindProperty("RightHandPreviewPrefab");

            ShowAngleGizmo = serializedObject.FindProperty("ShowAngleGizmo");
        }

        HandPoseType previousType;

        static GameObject lastPreviewRight;
        static GameObject lastPreviewLeft;

        public override void OnInspectorGUI() {

            grabPoint = (GrabPoint)target;
            bool inPrefabMode = false;
#if UNITY_EDITOR && (UNITY_2019 || UNITY_2020)
            inPrefabMode = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null;
#endif

            // Double check that there wasn't an object left in the scene
            checkForExistingPreview(); 

            // Check for change in handpose type
            if(grabPoint.handPoseType != previousType) {
                OnHandPoseTypeChange();
            }

            // Load the texture resource
            if (buttonLeftTexture == null) {
                buttonLeftTexture = (Texture)Resources.Load("handIcon", typeof(Texture));
                buttonLeftTextureSelected = (Texture)Resources.Load("handIconSelected", typeof(Texture));
                buttonRightTexture = (Texture)Resources.Load("handIconRight", typeof(Texture));
                buttonRightTextureSelected = (Texture)Resources.Load("handIconSelectedRight", typeof(Texture));
            }

            GUILayout.Label("Toggle Hand Preview : ", EditorStyles.boldLabel);

            if(inPrefabMode) {
                GUILayout.Label("(Some preview features disabled in prefab mode)", EditorStyles.largeLabel);
            }

            GUILayout.BeginHorizontal();

            // Take the right hand and invert it's values to get a left hand transform. Requires both left and right to valid since it is acting as both
            bool doLeftHandInvert = grabPoint.LeftHandIsValid && grabPoint.RightHandIsValid && grabPoint.InvertLeftHand;

            // Show / Hide Left Hand
            if (showingLeftHand) {

                // Define a GUIContent which uses the texture
                buttonLeftContent = new GUIContent(buttonLeftTextureSelected);

                if (!grabPoint.LeftHandIsValid || doLeftHandInvert || GUILayout.Button(buttonLeftContent)) {
                    if(LeftHandPreview != null) {
                        GameObject.DestroyImmediate(LeftHandPreview);
                    }
                    
                    showingLeftHand = false;
                }
            }
            else {
                buttonLeftContent = new GUIContent(buttonLeftTexture);

                if (grabPoint.LeftHandIsValid && !doLeftHandInvert && GUILayout.Button(buttonLeftContent)) {
                    // Create and add the Editor preview
                    CreateLeftHandPreview();
                }
            }

            // Show / Hide Right Hand
            if (showingRightHand) {

                // Define a GUIContent which uses the texture
                buttonRightContent = new GUIContent(buttonRightTextureSelected);

                if (!grabPoint.RightHandIsValid || GUILayout.Button(buttonRightContent)) {
                    GameObject.DestroyImmediate(RightHandPreview);
                    showingRightHand = false;
                }
            }
            else {
                buttonRightContent = new GUIContent(buttonRightTexture);

                if (grabPoint.RightHandIsValid && GUILayout.Button(buttonRightContent)) {
                    CreateRightHandPreview();
                }
            }

            GUILayout.EndHorizontal();

            updateEditorAnimation();

            EditorGUILayout.PropertyField(LeftHandIsValid);

            EditorGUILayout.PropertyField(RightHandIsValid);

            if(grabPoint.LeftHandIsValid && grabPoint.RightHandIsValid) {
                EditorGUILayout.PropertyField(InvertLeftHand);

                if(grabPoint.InvertLeftHand) {
                    EditorGUILayout.PropertyField(InvertLeftPositionX);
                    EditorGUILayout.PropertyField(InvertLeftPositionY);
                    EditorGUILayout.PropertyField(InvertLeftPositionZ);

                    EditorGUILayout.PropertyField(InvertLeftEulerX);
                    EditorGUILayout.PropertyField(InvertLeftEulerY);
                    EditorGUILayout.PropertyField(InvertLeftEulerZ);
                }
            }

            EditorGUILayout.PropertyField(handPoseType);

            if(grabPoint.handPoseType == HandPoseType.HandPose) {
                EditorGUILayout.PropertyField(SelectedHandPose);

                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("");
                EditorGUILayout.Space(0, true);

                if (GUILayout.Button("Edit Pose...")) {
                    EditHandPose();
                }
                GUILayout.EndHorizontal();

            }
            else if (grabPoint.handPoseType == HandPoseType.AnimatorID) {
                EditorGUILayout.PropertyField(HandPose);
            }
            
            //EditorGUILayout.PropertyField(HandPosition);
            EditorGUILayout.PropertyField(MaxDegreeDifferenceAllowed);
            EditorGUILayout.PropertyField(IndexBlendMin);
            EditorGUILayout.PropertyField(IndexBlendMax);
            EditorGUILayout.PropertyField(ThumbBlendMin);
            EditorGUILayout.PropertyField(ThumbBlendMax);

            EditorGUILayout.PropertyField(ShowAngleGizmo);

            // Preview name option
            EditorGUILayout.PropertyField(LeftHandPreviewPrefab);


            if (grabPoint.LeftHandPreviewPrefab == null && lastPreviewLeft != null) {
                if (GUILayout.Button("Set to " + lastPreviewLeft.name)) {
                    grabPoint.LeftHandPreviewPrefab = lastPreviewLeft;
                }
            }

            // Update any successful previews
            if (grabPoint.LeftHandPreviewPrefab != null) {
                lastPreviewLeft = grabPoint.LeftHandPreviewPrefab;
            }

            EditorGUILayout.PropertyField(RightHandPreviewPrefab);

            if (grabPoint.RightHandPreviewPrefab == null && lastPreviewRight != null) {
                if (GUILayout.Button("Set to " + lastPreviewRight.name)) {
                    grabPoint.RightHandPreviewPrefab = lastPreviewRight;
                }
            }


            if (grabPoint.RightHandPreviewPrefab != null) {
                lastPreviewRight = grabPoint.RightHandPreviewPrefab;
            }

            serializedObject.ApplyModifiedProperties();
            // base.OnInspectorGUI();
        }

        public void OnHandPoseTypeChange() {
            if(grabPoint.handPoseType == HandPoseType.HandPose) {
                UpdateHandPosePreview();
            }
            previousType = grabPoint.handPoseType;
        }

        public void CreateRightHandPreview() {
            // Create and add the Editor preview
            if(grabPoint.RightHandPreviewPrefab != null) {
                RightHandPreview = Instantiate(grabPoint.RightHandPreviewPrefab);
            } 
            else {
                RightHandPreview = Instantiate(Resources.Load(_defaultRightModelPreviewName, typeof(GameObject))) as GameObject;
            }

            RightHandPreview.transform.name = RightHandPreview.transform.name.Replace("(Clone)", "");
            RightHandPreview.transform.parent = grabPoint.transform;
            RightHandPreview.transform.localPosition = Vector3.zero;
            RightHandPreview.transform.localEulerAngles = Vector3.zero;
            RightHandPreview.gameObject.hideFlags = HideFlags.HideAndDontSave;
            //RightHandPreview.gameObject.hideFlags = HideFlags.DontSave;

#if UNITY_EDITOR
            if (grabPoint != null) {
                grabPoint.UpdatePreviews();
            }
#endif
            showingRightHand = true;
        }

        public void CreateLeftHandPreview() {

            if (grabPoint.LeftHandPreviewPrefab != null) {
                Debug.Log("Creating Left Hand Preview Prefab");
                LeftHandPreview = Instantiate(grabPoint.LeftHandPreviewPrefab);

            } 
            else {
                LeftHandPreview = Instantiate(Resources.Load(_defaultLeftModelPreviewName, typeof(GameObject))) as GameObject;
            }

            LeftHandPreview.transform.name = LeftHandPreview.transform.name.Replace("(Clone)", "");
            LeftHandPreview.transform.parent = grabPoint.transform;
            LeftHandPreview.transform.localPosition = Vector3.zero;
            LeftHandPreview.transform.localEulerAngles = Vector3.zero;
            LeftHandPreview.gameObject.hideFlags = HideFlags.HideAndDontSave;
            // LeftHandPreview.gameObject.hideFlags = HideFlags.DontSave;

#if UNITY_EDITOR
            if (grabPoint != null) {
                grabPoint.UpdatePreviews();
            }
#endif
            showingLeftHand = true;
        }

        public void EditHandPose() {
            // Select the Hand Object
            if(grabPoint.RightHandIsValid) {
                if(!showingRightHand) {
                    CreateRightHandPreview();
                }

                RightHandPreview.gameObject.hideFlags = HideFlags.DontSave;
                HandPoser hp = RightHandPreview.gameObject.GetComponentInChildren<HandPoser>();
                if(hp != null) {
                    Selection.activeGameObject = hp.gameObject;
                }
            }
            else if (grabPoint.LeftHandIsValid) {
                if (!showingLeftHand) {
                    CreateLeftHandPreview();
                }

                LeftHandPreview.gameObject.hideFlags = HideFlags.DontSave;
                HandPoser hp = LeftHandPreview.gameObject.GetComponentInChildren<HandPoser>();
                if (hp != null) {
                    Selection.activeGameObject = hp.gameObject;
                }
            }
            else {
                Debug.Log("No HandPoser component was found on hand preview prefab. You may need to add one to 'Resources/RightHandModelsEditorPreview'.");
            }
        }

        void updateEditorAnimation() {

            if (LeftHandPreview) {
                var anim = LeftHandPreview.GetComponentInChildren<Animator>();
                updateAnimator(anim, (int)grabPoint.HandPose);
            }

            if (RightHandPreview) {
                var anim = RightHandPreview.GetComponentInChildren<Animator>();

                updateAnimator(anim, (int)grabPoint.HandPose);
            }
        }

        public void UpdateHandPosePreview() {
            if (LeftHandPreview) {
                var hp = LeftHandPreview.GetComponentInChildren<HandPoser>();
                if(hp) {
                    // Trigger a change
                    hp.OnPoseChanged();
                }
            }
            if (RightHandPreview) {
                var hp = RightHandPreview.GetComponentInChildren<HandPoser>();
                if (hp) {
                    hp.OnPoseChanged();
                }
            }
        }

        void updateAnimator(Animator anim, int handPose) {
            if (anim != null && anim.isActiveAndEnabled && anim.gameObject.activeSelf) {

                // Do Fist Pose
                if (handPose == 0) {

                    // 0 = Hands Open, 1 = Grip closes                        
                    anim.SetFloat("Flex", 1);

                    anim.SetLayerWeight(0, 1);
                    anim.SetLayerWeight(1, 0);
                    anim.SetLayerWeight(2, 0);
                }
                else {
                    anim.SetLayerWeight(0, 0);
                    anim.SetLayerWeight(1, 0);
                    anim.SetLayerWeight(2, 0);
                }

                anim.SetInteger("Pose", handPose);
                anim.Update(Time.deltaTime);

#if UNITY_EDITOR && (UNITY_2019 || UNITY_2020)
                // Only set dirty if not in prefab mode
                if (UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null) {
                    UnityEditor.EditorUtility.SetDirty(anim.gameObject);
                }
#endif
            }
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/VRIF/GrabPoint", false, 11)]
        private static void AddGrabbable(MenuCommand menuCommand) {
            // Create and add a new Grabbable Object in the Scene
            GameObject grabPoint = Instantiate(Resources.Load("DefaultGrabPointItem", typeof(GameObject))) as GameObject;
            grabPoint.name = "GrabPoint";

            GameObjectUtility.SetParentAndAlign(grabPoint, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(grabPoint, "Created GrabPoint " + grabPoint.name);
            Selection.activeObject = grabPoint;
        }
#endif
        void checkForExistingPreview() {
            if (LeftHandPreview == null && !showingLeftHand) {
                Transform lt = grabPoint.FindLeftHandPreview();
                if (lt) {
                    LeftHandPreview = lt.gameObject;
                    showingLeftHand = true;
                }
            }

            if (RightHandPreview == null && !showingRightHand) {
                Transform rt = grabPoint.FindRightHandPreview();
                if (rt) {
                    RightHandPreview = rt.gameObject;
                    showingRightHand = true;
                }
            }
        }
    }
}

