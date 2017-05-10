namespace Linehaul_Helper.Models
{
    public class PsiKgPair
    {
        public int Psi;
        public int Kilos;

        public override string ToString()
        {
            return $"{Psi} PSI ({Kilos}kg)";
        }
    }
}
