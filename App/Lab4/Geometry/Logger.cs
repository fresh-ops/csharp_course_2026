namespace App.Lab4.Geometry;

public static class Logger
{
    public static string LogGeometry(IGeometry geometry) => geometry switch
    {
        Triangle => "треугольник",
        Square => "квадрат",
        Rectangle => "прямоугольник",
        _ => "неизвестная geometry",
    };
}
