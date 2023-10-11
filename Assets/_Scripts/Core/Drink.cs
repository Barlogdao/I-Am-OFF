using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Drink : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;
    private Transform _parent;

    private Vector3 _localTransform;
    private DrinkSO _drinkData;
    private HumanPlayer _player;
    [SerializeField] private SpriteRenderer _questionLabel;
    private bool _isDrinkTaken = false;
    private Tweener _tweener;
    private Transform _transform;

    public static event Func<DrinkSO> DrinkChanged;

    private bool _isDrunkAsHellStage = false;
    private WaitForSeconds _respawnTime = new WaitForSeconds(1.2f);

    public static Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void Init(Game game, Transform parent)
    {
        _player = game.Player;
        _parent = parent;
        _drinkData = DrinkChanged?.Invoke();
        _spriteRenderer.sprite = _drinkData.Image;
        _circleCollider.enabled = true;
        _spriteRenderer.enabled = true;
        HumanPlayer.PlayerMindChanged += OnPlayerOFF;
        Game.GameOvered += OnGameOver;
        Game.GameStarted += OnGameStarted;
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;

        _questionLabel.enabled = false;
    }

    private void Awake()
    {
        _transform = transform;
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _questionLabel.enabled = false;
    }

    private void OnGameStarted()
    {
        StartCoroutine(DrinkUpdate());
    }

    private void OnSobrietyChanged(SobrietyLevel sobrietyLevel)
    {
        DrinkVisibility(sobrietyLevel);
    }

    private void OnPlayerOFF(bool playerIsOFF)
    {
        _circleCollider.enabled = !playerIsOFF;
        _spriteRenderer.color = playerIsOFF ? Color.gray : Color.white;
        _questionLabel.color = playerIsOFF ? Color.gray : Color.white;
    }

    private void OnMouseDown()
    {
        _localTransform = _transform.localPosition;
        _transform.parent = null;
        _isDrinkTaken = true;
        if (_isDrunkAsHellStage)
        {
            _spriteRenderer.enabled = true;
            _questionLabel.enabled = false;
        }
    }

    private void OnMouseDrag()
    {
        _transform.position = GetMousePos();
    }

    private void OnMouseUp()
    {
        if (_circleCollider.IsTouching(_player.PlayerCollider) && Game.Instance.State != GameState.GameOver && !_player.PlayerIsDrinking)
        {
            _player.Drink(_drinkData);
            StartCoroutine(DestroyDrink());
        }
        else
        {
            ReturnDrinkToWheel();
        }

        _isDrinkTaken = false;
        if (_isDrunkAsHellStage)
        {
            _spriteRenderer.enabled = false;
            _questionLabel.enabled = true;
        }
    }

    private void LateUpdate()
    {
        _transform.rotation = Quaternion.identity;
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
        _circleCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _questionLabel.enabled = false;
    }

    private void OnDestroy()
    {
        Game.GameOvered -= OnGameOver;
        HumanPlayer.PlayerMindChanged -= OnPlayerOFF;
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
        Game.GameStarted -= OnGameStarted;
    }

    private IEnumerator DestroyDrink()
    {
        // Напито обнуляется и становится недоступен
        ReturnDrinkToWheel();
        _circleCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _questionLabel.enabled = false;

        // проходит время
        yield return _respawnTime;

        // обновляется СО
        _drinkData = DrinkChanged?.Invoke();

        // появляется новый Дринк
        _transform.localScale = Vector3.zero;
        _transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
        _circleCollider.enabled = !_player.PlayerIsOFF;
        _spriteRenderer.enabled = true;
        DrinkVisibility(_player.Sobriety);
        _spriteRenderer.sprite = _drinkData.Image;
    }

    private void ReturnDrinkToWheel()
    {
        _transform.parent = _parent;
        _transform.localPosition = _localTransform;
    }

    private IEnumerator DrinkUpdate()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));
        while (Game.Instance.State != GameState.GameOver)
        {
            if (_isDrinkTaken == false)
            {
                _drinkData = DrinkChanged?.Invoke();
                _spriteRenderer.sprite = _drinkData.Image;
                _transform.localScale = Vector3.zero;
                _transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));
        }
    }

    private void DrinkVisibility(SobrietyLevel sobrietyLevel)
    {
        _isDrunkAsHellStage = sobrietyLevel == SobrietyLevel.DrunkAsHell;
        _questionLabel.enabled = _isDrunkAsHellStage;
        _spriteRenderer.enabled = !_isDrunkAsHellStage || _isDrinkTaken;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDrinkTaken)
        {
            _tweener = _transform.DOScale(1.15f, 0.2f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_tweener != null)
        {
            _tweener.Kill();
        }

        _transform.localScale = Vector3.one;
    }
}
