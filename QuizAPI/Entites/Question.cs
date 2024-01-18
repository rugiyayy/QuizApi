namespace QuizAPI.Entites
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; } //que
        public decimal Points { get; set; }


        //navigation
        public int QuizId { get; set; }
        public Quiz Quizzes { get; set; }
        public List<Option> Options { get; set; } 

    }
}
