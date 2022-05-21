using System.Collections;
using System.Collections.Generic;
using Scripts.Input;
using Scripts.Observer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class LevelManager : MonoBehaviour, IObservable<bool>
    {
        [SerializeField] private MainCharacterController _mainCharacter;
        [SerializeField] private FireController _fireController;
        [SerializeField] private GameObject _startMessage;
        [SerializeField] private InteractableCanvasObject _input;

        // Used Observer pattern. Alternetively could use dependency injection
        private List<IObserver<bool>> _observers;
        private bool _isGameplayOn;


        private void Awake()
        {
            _observers = new List<IObserver<bool>>();
            _mainCharacter.OnLastWPReached += EndLevel;
            _input.OnPointerDownEvent += OnTap;
            _isGameplayOn = false;
            AddObserver(_mainCharacter);
            AddObserver(_fireController);
        }

        private void OnTap(Vector3 position)
        {
            if (_isGameplayOn) return;
            _startMessage.gameObject.SetActive(false);
            StartCoroutine(StartGameplay());
        }

        private IEnumerator StartGameplay()
        {
            yield return new WaitForSeconds(0.5f);
            _isGameplayOn = true;
            NotifyObservers();
        }

        private void EndLevel()
        {
            RestartLevel();
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddObserver(IObserver<bool> o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver<bool> o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
                observer.UpdateObservableData(_isGameplayOn);
        }

        private void OnDestroy()
        {
            _mainCharacter.OnLastWPReached -= EndLevel;
            _input.OnPointerDownEvent -= OnTap;
        }
    }
}