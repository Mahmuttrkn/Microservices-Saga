using MassTransit;

namespace SagaMachine
{
    public class OrderStateMachine: MassTransitStateMachine<OrderStateInstance>
    {
        public OrderStateMachine()
        {
            //State Instance'da ki hangi property'nin sipariş sürecindeki state'i tutacağı bildiriliyor.
            //Yani artık tüm event'ler CurrentState property'sin de tutulacaktır!
            InstanceState(instance => instance.CurrentState);


            /*
                .
                Event çalışmaları
                .
             */
        }
    }
}
