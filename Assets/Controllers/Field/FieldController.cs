using Components.Notifications;
using DefaultNamespace;
using Models.Field;
using UnityEngine;
using Views.Cell;

namespace Controllers.Field
{
    public class FieldController : Element
    {
        public void IsLoad(params object[] pData)
        {
            app.model.field.LoadFieldData();
        }
        public void CellIsLoad(params object[] pData)
        {
            app.model.field.AddCell((Object) pData[0]);
        }
        public void CellResouceGenerated(params object[] pData)
        {
            var data = (object[]) pData[1];
            var count = (int) data[0];
            var view = (CellView) pData[0];
            var model = app.model.field.FindCellModel(view);
            model.NewResource(count);
            app.model.field.SaveField();
            view.SetResourceSprite(model);
        }
        public void CellClick(params object[] pData)
        {
            if (app.view.ui.ModalIsOpen)
                return;
            CellView cellView;
            if (app.model.field.activeCell != (Object) pData[0] && app.model.field.activeCell != null)
            {
                cellView = (CellView) app.model.field.activeCell;
                cellView.SetActiveState(false);
            }
            cellView = (CellView) pData[0];
            app.model.field.GetCellModel(cellView);
            // cellView.SetSprite(app.model.field.ActiveCellModel.ActiveSprite);

            app.Notify(Notification.ModalCellOpen,cellView);
        }

        public void CellEat(object[] pData)
        {
            var data = (object[]) pData[1];
            var count = (int) data[0];
            var model = app.model.field.FindCellModel((CellView) pData[0]);
            if (!app.model.ResourceExists(model.ResourceName))
                return;
            var resource = app.model.ResourcesDictionary[model.ResourceName];
            if (app.model.player.GetResourceCount(resource.FoodItem.Name) <= 0)
                return;
            var cellView = (CellView) pData[0];
            cellView.CellFeed();

            app.model.player.DeleteResource(resource.FoodItem.Name, 1);
        }

        public void CellFoodChange(object[] pData)
        {
            var data = (object[]) pData[1];
            CellModel model = app.model.field.FindCellModel((Object) pData[0]);
            model.foodCount = (int) data[0];
            app.model.field.SaveField();
        }
    }
}