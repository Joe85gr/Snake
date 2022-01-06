using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Models;
using UnityEngine;

namespace Services
{
    public class ScoreService : MonoBehaviour
    {
        public ScoreRow scoreRow;
        public GameObject scoreRows;
        public GameObject scoreHeader;

        public void Show()
        {
            var scores = GetScores();

            GenerateHeader();
            GenerateRows(scores);
            
            scoreRows.SetActive(true);
            scoreHeader.SetActive(true);
        }

        public void Hide()
        {
            while (scoreRows.transform.childCount > 0)
            {
                DestroyImmediate(scoreRows.transform.GetChild(0).gameObject);
            }
            
            scoreRows.SetActive(false);
            scoreHeader.SetActive(false);
        }

        private static List<(string Name, float Score)> GetScores()
        {
            var scores = SaveService.Load()
                .Scores
                .OrderByDescending(x => x.Score)
                .ToList();

            return scores;
        }

        private void GenerateHeader()
        {
            var headerRow = Instantiate(scoreRow, transform).GetComponent<ScoreRow>();

            headerRow.place.text = headerRow.place.name.ToUpper();
            headerRow.player.text = headerRow.player.name.ToUpper();
            headerRow.score.text = headerRow.score.name.ToUpper();
        }

        private void GenerateRows(IReadOnlyList<(string Name, float Score)> scores)
        {
            for (var i = 0; i < scores.Count; i++)
            {
                var row = Instantiate(scoreRow, transform).GetComponent<ScoreRow>();
                
                row.place.text = $"{i + 1}";
                row.player.text = scores[i].Name;
                row.score.text = scores[i].Score.ToString(CultureInfo.InvariantCulture);
            }
        }

    }
}
