using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTrack
{
    public static class Constantes
    {
        // Deben concordar con los valores ENUM en la base de datos para Pacientes
        public static class Sexos
        {
            public const string Masculino   = "M";
            public const string Femenino    = "F";
            public const string Trans       = "X";
            public const string NoDeclarado = "N/D";
        }
        public static class NombreSexos
        {
            public const string Masculino   = "Masculino";
            public const string Femenino    = "Femenino";
            public const string Trans       = "Transexual";
            public const string NoDeclarado = "No declarado";
        }

        // Deben concordar con los IDs en la tabla Roles de la base de datos
        public static class Roles
        {
            public const uint ADMINISTRADOR = 1;
            public const uint MEDICO        = 2;
            public const uint ENFERMERA     = 3;
            public const uint RECEPCIONISTA = 4;
            public const uint PACIENTE      = 5;
        }
    }
}
