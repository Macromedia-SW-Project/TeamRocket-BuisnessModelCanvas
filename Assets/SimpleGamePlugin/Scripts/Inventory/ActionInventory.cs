namespace Course.PrototypeScripting
{
    public class ActionInventory : Action
    {
        public string invItemName;

        public enum ActionType { Add, Sub, SetExplicit }
        public ActionType type;

        public int amount;

        override public void ExecuteAction()
        {
            switch (type)
            {
                case ActionType.Add:
                    InventoryManager.Instance.AddInvItem(invItemName, amount);
                    break;
                case ActionType.Sub:
                    InventoryManager.Instance.SubInvItem(invItemName, amount);
                    break;
                case ActionType.SetExplicit:
                    InventoryManager.Instance.SetExplicitInvItem(invItemName, amount);
                    break;
            }
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {
            switch (type)
            {
                case ActionType.Add:
                    return invItemName + ":  +" + amount;
                case ActionType.Sub:
                    return invItemName + ":  -" + amount;
                case ActionType.SetExplicit:
                    return invItemName + ": " + amount;
            }
            return "";
        }
    }
}
