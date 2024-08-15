using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
  public class ButtonAnimatedClick : MonoBehaviour
  {
    [Header("References")]
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Text _textElement;
    [SerializeField] private RectTransform _playIconRect;

    [Header("Animation Settings")]
    [SerializeField] private float _upPosition = 0f;
    [SerializeField] private float _downPositon = -12f;
    [SerializeField] private float _animationDuration = 0.1f;

    [Header("Color Settings")]
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private float _colorChangeDuration = 0.1f;

    [Header("Icon Movement Settings")]
    [SerializeField] private float _iconOffset = 10f;

    private Button _button;

    private void Awake() {
      _button = GetComponent<Button>();
    }

    private void OnEnable() {
      _button.onClick.AddListener(ClickAnimation);
    }

    private void Start() {
      UpdateButtonPosition();
    }

    private void Update() {
      UpdateButtonPosition();
    }

    private void UpdateButtonPosition() {
      float textWidth = _textElement.preferredWidth;

      _playIconRect.anchoredPosition = new Vector2(
        -textWidth / 2 - _playIconRect.rect.width / 2 - _iconOffset, _playIconRect.anchoredPosition.y);
    }

    private void ClickAnimation() {
      Sequence pressSeq = DOTween.Sequence();
      Sequence colorSeq = DOTween.Sequence();

      pressSeq.Append(_buttonImage.transform.DOLocalMoveY(_downPositon, _animationDuration));
      pressSeq.Append(_buttonImage.transform.DOLocalMoveY(_upPosition, _animationDuration))
          .SetEase(Ease.OutSine);

      colorSeq.Append(_buttonImage.DOColor(_pressedColor, _colorChangeDuration));
      colorSeq.Append(_buttonImage.DOColor(_defaultColor, _colorChangeDuration));

      pressSeq.Play();
      colorSeq.Play();
    }
  }
}