# Frano NPC Mod - Terraria (tModLoader)

Mod modular de Terraria que agrega a **Frano**, un NPC misterioso con tienda y sistema de quests.

## Características

### Frano - NPC
- Aparece **atado** en la superficie al iniciar el mundo (como el Goblin Tinkerer)
- Sprite: en shorts y sin remera
- Al liberarlo da una **mini-recompensa** (monedas, pociones, antorchas)
- Se instala como Town NPC con tienda

### Tienda
- **Base**: Gel, Antorchas, Soga
- **Post Quest 1** ("Escapar de la ley"): Shurikens, Cuchillos, Granadas, Bombas, Barras de hierro
- **Post Quest 2** ("Traer la ropa"): Balas de Pesto, Musket Balls, Silver Bullets

### Sistema de Quests (Modular)

El sistema de quests es extensible. Cada quest es una clase que hereda de `BaseQuest` y se registra en `QuestManager`.

#### Quest 1: "Escapar de la ley"
1. Frano marca un cofre aleatorio en el mapa
2. El cofre contiene **"Orden de detención: Frano"**
3. Al entregarlo, Frano expande su tienda

#### Quest 2: "Traer la ropa"
1. Frano marca un cofre aleatorio (no tan profundo)
2. El cofre contiene **"El Traje"**
3. Al entregarlo:
   - Frano cambia de textura (se viste de **traje smoking**)
   - Entrega **"Pesto"**, un revólver verde gastado
   - Empieza a vender **Balas de Pesto** en su tienda

### Pesto (Arma)
- Revólver básico verde gastado
- 14 de daño ranged
- Usa **Bala de Pesto** como munición exclusiva
- Las balas se pueden craftear (50 Musket Balls + Green Dye en Anvil) o comprar a Frano post-Quest 2

## Agregar nuevos Quests

El sistema es modular. Para agregar un quest nuevo:

1. Crear una clase en `Common/Quests/` que herede de `BaseQuest`:

```csharp
public class MiNuevoQuest : BaseQuest
{
    public override string Name => "Nombre del Quest";
    public override string Description => "Lo que Frano le dice al jugador...";
    public override string CompletionMessage => "Lo que dice al completarlo...";
    public override int RequiredItemType => ModContent.ItemType<MiItem>();
    public override double MaxChestDepth => Main.worldSurface; // Profundidad máx del cofre

    public override void OnComplete(Player player, NPC frano)
    {
        // Recompensas
    }
}
```

2. Registrarlo en `FranoWorldSystem.RegisterQuests()`:
```csharp
QuestManager.RegisterQuest(new MiNuevoQuest());
```

## Estructura del Proyecto

```
FranoMod/
├── FranoMod.cs                          # Clase principal del mod
├── FranoMod.csproj                      # Proyecto .NET
├── build.txt                            # Metadata del mod
├── description.txt                      # Descripción
├── Content/
│   ├── NPCs/
│   │   ├── BoundFrano.cs                # NPC atado (superficie)
│   │   └── Frano.cs                     # Town NPC (tienda + quests)
│   ├── Items/
│   │   ├── OrdenDeDetencion.cs          # Item quest 1
│   │   ├── ElTraje.cs                   # Item quest 2
│   │   ├── Pesto.cs                     # Revólver
│   │   └── BalaPesto.cs                 # Munición
│   └── Projectiles/
│       └── PestoProjectile.cs           # Proyectil de bala
├── Common/
│   ├── Quests/
│   │   ├── BaseQuest.cs                 # Clase abstracta base
│   │   ├── QuestManager.cs              # Gestor de quests
│   │   ├── EscaparDeLaLey.cs            # Quest 1
│   │   └── TraerLaRopa.cs               # Quest 2
│   └── Systems/
│       └── FranoWorldSystem.cs          # Estado del mundo + mapa
└── Localization/
    ├── es-ES.hjson                      # Español
    └── en-US.hjson                      # Inglés
```

## Texturas Requeridas

El mod necesita las siguientes texturas PNG para funcionar:

| Archivo | Descripción | Tamaño sugerido |
|---------|-------------|-----------------|
| `Content/NPCs/Frano.png` | Frano en shorts, sin remera (town NPC spritesheet) | 40x1120 (40x56 x 20 frames) |
| `Content/NPCs/Frano_Suited.png` | Frano con traje smoking (misma estructura) | 40x1120 |
| `Content/NPCs/Frano_Head.png` | Icono del mapa (cabeza default) | 36x36 |
| `Content/NPCs/BoundFrano.png` | Frano atado en shorts | 40x56 |
| `Content/Items/OrdenDeDetencion.png` | Papel/documento | 32x32 |
| `Content/Items/ElTraje.png` | Traje de smoking | 32x32 |
| `Content/Items/Pesto.png` | Revólver verde gastado | 38x24 |
| `Content/Items/BalaPesto.png` | Bala verde | 14x14 |
| `Content/Projectiles/PestoProjectile.png` | Proyectil de bala | 8x8 |

## Instalación

1. Clonar el repositorio en `Documents/My Games/Terraria/tModLoader/ModSources/`
2. Agregar las texturas PNG en las rutas indicadas
3. Compilar desde tModLoader (Mod Sources → Build)

## Requisitos

- Terraria 1.4.4+
- tModLoader 1.4.4+
- .NET 6.0
