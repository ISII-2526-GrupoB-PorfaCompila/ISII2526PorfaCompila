using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ReviewStateContainer
    {

        //we create an instance of Review when an instance of ReviewStateContainer is created
        public ReviewForCreateDTO Review { get; private set; } = new ReviewForCreateDTO()
        {
            ReviewItems = new List<ReviewItemDTO>()
        };

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddCarToReview(CarForReviewDTO car)
        {
            //before adding a cqr we checked whether it has been already added
            if (!Review.ReviewItems.Any(ri => ri.CarId == car.Id))
                //we add it if it is not in the list
                Review.ReviewItems.Add(new ReviewItemDTO()
                {
                    CarId = car.Id,
                    Model = car.Model,
                    Manufacturer = car.Manufacturer,
                    Color = car.Color,
                }
            );

        }

        //to delete cars from the list of selected movies
        public void RemoveReviewItemToReview(ReviewItemDTO item)
        {
            Review.ReviewItems.Remove(item);

        }

        //we eliminate all the cars from the list
        public void ClearReviewingCart()
        {
            Review.ReviewItems.Clear();

        }

        //we have already finished the process of reviewing, thus, we create a new Review 
        public void ReviewProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Review = new ReviewForCreateDTO()
            {
                ReviewItems = new List<ReviewItemDTO>()
            };
        }
    }
}