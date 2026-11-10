using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    // De tre valgmuligheder hører sammen, derfor er de samlet i en enkelt record. En record er immutable.
    public record SearchOptions // Üç seçenek birbirine ait, bu yüzden tek bir record'da toplandı. Record immutable'dır.
    (
        bool CaseSensitive, // true: "Type" og "type" betragtes som forskellige
        bool WholeWord , // true: kun hele ord tælles ("type" i "types" tælles ikke)
        bool showElapsedTime // true: varigheden vises på skærmen
    );
    
    
}
