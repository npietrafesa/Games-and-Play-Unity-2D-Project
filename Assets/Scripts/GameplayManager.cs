using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Text HistoryText;
    public Transform AnswersParent;
    public GameObject ButtonAnswerPrefab;
    public Image background;
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject ship;
    public GameObject crashed_ship;
    public GameObject pilot;
    public GameObject alien1;
    public GameObject alien2;
    public GameObject alien3;
    public GameObject alienking;
    public GameObject newship;
    private StoryNode currentNode;
    private Scenes scene = Scenes.SCENE1;

    private void Start()
    {
        currentNode = StoryFiller.FillStory();
        HistoryText.text = string.Empty;
        FillUi();
    }

    private void changeBackground(int index)
    {
        if (index == 1)
            background.sprite = sprite1;
        else
            background.sprite = sprite2;
    }

    private void FillUi()
    {
        HistoryText.text = currentNode.History;

        foreach (Transform child in AnswersParent.transform) Destroy(child.gameObject);

        var isLeft = true;
        var height = 0f;
        var index = 0;
        foreach (var answer in currentNode.Answers)
        {
            var buttonAnswerCopy = Instantiate(ButtonAnswerPrefab, AnswersParent, true);

            var x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.3f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft)
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 3.0f;
            isLeft = !isLeft;

            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);

            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

            index++;
        }
    }

    private void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() => { AnswerSelected(index); });
    }

    private void AnswerSelected(int index)
    {
        //HistoryText.text += "\n" + currentNode.Answers[index];
        //HistoryText.text = "\n" + currentNode.Answers[index];

        if (!currentNode.IsFinal && !currentNode.LoseGame)
        {
            currentNode = currentNode.NextNode[index];

            currentNode.OnNodeVisited?.Invoke();

            //changeBackground(currentNode.Index);

            FillUi();

            ChangeScenes(currentNode.Index);

            switch (scene)
            {
                case Scenes.SCENE1: //crash
                    changeBackground(1);
                    ship.SetActive(false);
                    crashed_ship.SetActive(true);
                    pilot.SetActive(true);
                    break;
                case Scenes.SCENE2: //find aliens
                    crashed_ship.SetActive(false);
                    alien1.SetActive(true);
                    alien2.SetActive(true);
                    alien3.SetActive(true);
                    break;
                case Scenes.SCENE3: //stay on ship
                    crashed_ship.SetActive(true);
                    alien1.SetActive(false);
                    alien2.SetActive(false);
                    alien3.SetActive(false);
                    pilot.transform.Rotate(Vector3.forward * -90);
                    break;
                case Scenes.SCENE4: //meet king
                    changeBackground(2);
                    alien1.SetActive(false);
                    alien2.SetActive(false);
                    alien3.SetActive(false);
                    alienking.SetActive(true);
                    break;
                case Scenes.SCENE5: //you die
                    pilot.transform.Rotate(Vector3.forward * -90);
                    break;
                case Scenes.SCENE6: //you escape
                    newship.SetActive(true);
                    break;
            }
        }
        else if (currentNode.IsFinal)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void ChangeScenes(int index)
    {
        scene = (Scenes)index;
    }

    private enum Scenes
    {
        SCENE1 = 1,
        SCENE2 = 2,
        SCENE3 = 3,
        SCENE4 = 4,
        SCENE5 = 5,
        SCENE6 = 6
    }
}