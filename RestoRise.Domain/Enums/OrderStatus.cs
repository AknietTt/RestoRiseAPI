namespace RestoRise.Domain.Enums;

public enum OrderStatus
{
    Pending, // В ожидании
    Accepted, // Принято
    Done, // Готово
    WithCourier, // У курьера
    Delivered ,// Доставлено
    Cancel = 99 
}