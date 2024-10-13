using ECommerce.Model;
using Microsoft.AspNetCore.Mvc;


namespace FirstMVC.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _unitOfWork.Categories.GetAll();
        return View(categories);  
        }
    }
}
