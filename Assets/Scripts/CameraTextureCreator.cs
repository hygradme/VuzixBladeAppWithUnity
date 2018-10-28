using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UI;
public class CameraTextureCreator : MonoBehaviour
{
	[SerializeField]
	private int     m_width     = 1920;
	[SerializeField]
	private int     m_height    = 1080;
	[SerializeField]
	private RawImage    m_displayUI = null;
	[SerializeField]
	private Text cameratext;

	private WebCamTexture   m_webCamTexture     = null;


	private IEnumerator Start()
	{
		if( WebCamTexture.devices.Length == 0 )
		{
			cameratext.text="no camera device";

			yield break;
		}

		yield return Application.RequestUserAuthorization( UserAuthorization.WebCam );
		if( !Application.HasUserAuthorization( UserAuthorization.WebCam ) )
		{
			cameratext.text="Not allow to use Camera device";
			yield break;
		}

		WebCamDevice userCameraDevice = WebCamTexture.devices[ 0 ];
		m_webCamTexture = new WebCamTexture( userCameraDevice.name, m_width, m_height );

		m_displayUI.texture = m_webCamTexture;

		m_webCamTexture.Play();

		cameratext.text="Using Camera device";
	}
} // class TestCamera