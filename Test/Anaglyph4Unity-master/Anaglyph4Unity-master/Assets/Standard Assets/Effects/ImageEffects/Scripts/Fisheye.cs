using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [RequireComponent (typeof(Camera))]
    [AddComponentMenu ("Image Effects/Displacement/Fisheye")]
    public class Fisheye : PostEffectsBase
	{
        [Range(0.0f, 1.5f)]
        public float strengthX = 0.05f;
        [Range(0.0f, 1.5f)]
        public float strengthY = 0.05f;

		public RenderTexture RightEye;
		public RenderTexture LeftEye;
        public Shader fishEyeShader = null;
        private Material fisheyeMaterial = null;


        public override bool CheckResources ()
		{
            CheckSupport (false);
            fisheyeMaterial = CheckShaderAndCreateMaterial(fishEyeShader,fisheyeMaterial);

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }

        void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
            if (CheckResources()==false)
			{
                Graphics.Blit (source, destination);
                return;
            }

            float oneOverBaseSize = 80.0f / 512.0f; // to keep values more like in the old version of fisheye

            float ar = (source.width * 1.0f) / (source.height * 1.0f);

			fisheyeMaterial.SetTexture ("_RightEye",RightEye);
			fisheyeMaterial.SetTexture ("_LeftEye",LeftEye);
            Graphics.Blit (source, destination, fisheyeMaterial);
        }
    }
}
