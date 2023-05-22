/*
 * Class:           IGameInfo.cs
 * Module:          ICallback, IGameInfo
 * Author:          Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date:            30/03/2023
 * Description:     Interfaces for callbacks and gameinfo
 */


using System.Collections.Generic;
using System.ServiceModel;
using System.Windows; 


namespace CatchMindLibrary 
{
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateClient(CallbackInfo info, int key);
    }

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IGameInfo
    {
        List<User> Users { [OperationContract] get; [OperationContract] set; }
        string BrushColour { [OperationContract] get; [OperationContract] set; }

        bool IsDrawing { [OperationContract] get; [OperationContract] set; }
        Point LastPoint { [OperationContract] get; [OperationContract] set; }
        double BrushThickness { [OperationContract] get; [OperationContract] set; }
        double X1 { [OperationContract] get; [OperationContract] set; }
        double Y1 { [OperationContract] get; [OperationContract] set; }
        double X2 { [OperationContract] get; [OperationContract] set; }
        double Y2 { [OperationContract] get; [OperationContract] set; }
        bool IsAnswerSet { [OperationContract] get; [OperationContract] set; }
        string Answer { [OperationContract] get; [OperationContract] set; }
        string SharedMessage { [OperationContract] get; [OperationContract] set; }
        bool IsClear { [OperationContract] get; [OperationContract] set; }
        bool IsGameOver { [OperationContract] get; [OperationContract] set; }


        [OperationContract]
        void AddUser(int id, bool isDrawer);
        [OperationContract]
        void AddScore(int id);
        [OperationContract]
        int RegisterForCallbacks();
        [OperationContract(IsOneWay = true)]
        void UnregisterFromCallbacks();
    }
}
