import React, { useEffect } from "react";
import OrderList from "../Orders/OrderList";
import { useDispatch, useSelector } from "react-redux";
import { cancelOrder, resetState } from "../../features/orders/ordersSlice";
import { notifySuccess, notifyError } from "../../utils/notify";

const BuyerDashboard = ({ children }) => {
  const dispatch = useDispatch();

  const { orders, isSuccess, isLoading, isError, message } = useSelector(
    (state) => state.orders
  );

  const isPreviousOrder = (order) => {
    const createdAt = new Date(order.createdAt);
    const deliveryTime = order.deliveryTime;
    const deliveredAt = new Date();
    deliveredAt.setHours(createdAt.getHours() + deliveryTime);

    return deliveredAt < new Date();
  };

  const previousOrders = orders.filter((order) => isPreviousOrder(order));
  const activeOrders = orders.filter((order) => !isPreviousOrder(order));

  useEffect(() => {
    if (isError && message) {
      notifyError(message);
    }

    if (isSuccess && message) {
      notifySuccess(message);
    }

    dispatch(resetState());
  }, [isSuccess, isLoading, isError, message, dispatch]);

  const handleDelete = (id) => {
    dispatch(cancelOrder(id));
  };

  return (
    <div>
      <h1>Buyer Dashboard</h1>
      <hr />
      {children}
      <hr />
      <div>
        <h3>Active Orders</h3>
        {activeOrders && activeOrders.length > 0 ? (
          <OrderList
            orders={activeOrders}
            canDelete={true}
            handleDelete={handleDelete}
          />
        ) : (
          <p>No Active Orders</p>
        )}
      </div>
      <hr />
      <div>
        <h3>Previous Orders</h3>
        {previousOrders && previousOrders.length > 0 ? (
          <OrderList orders={previousOrders} />
        ) : (
          <p>No Previous Orders</p>
        )}
      </div>
    </div>
  );
};

export default BuyerDashboard;