using System.Diagnostics.CodeAnalysis;

// Perpetually annoying and pointless things.
[assembly: SuppressMessage("Style", "IDE0008")] // No var
[assembly: SuppressMessage("Style", "IDE0022")] // No arrow fn syntax.
[assembly: SuppressMessage("Style", "IDE0025")]
[assembly: SuppressMessage("Style", "IDE0027")]
[assembly: SuppressMessage("Style", "IDE0040")] // Add access modifiers
[assembly: SuppressMessage("Style", "IDE0051")] // Method is unused.
[assembly: SuppressMessage("Style", "IDE0055")] // No Tab indents
[assembly: SuppressMessage("Style", "IDE0058")] // Expr is never used.
[assembly: SuppressMessage("Style", "IDE0061")] // Use method body, not arrow.
[assembly: SuppressMessage("Style", "IDE0072")] // Expand switch expressions.
[assembly: SuppressMessage("Style", "IDE0130")]
[assembly: SuppressMessage("Style", "IDE0160")] // No file namespacing.

// C#12 collection initialiser recommend (remove later).
[assembly: SuppressMessage("Style", "IDE0028")]

// C#12 collection initialiser recommend (remove later).
[assembly: SuppressMessage("Style", "IDE0300")]

// C#12 primary ctor recommend (remove later).
[assembly: SuppressMessage("Style", "IDE0290")]

// Plz simplify this if expression by making it harder to read.
[assembly: SuppressMessage("Style", "IDE0045")]
[assembly: SuppressMessage("Style", "IDE0046")]

// Plz add parentheses to make what is 99% obvious 100% obvious.
[assembly: SuppressMessage("Style", "IDE0048")]
