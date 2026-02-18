"""
Generador de texturas pixel art para el mod FranoMod (tModLoader).
Crea todos los PNGs necesarios para que el mod compile y funcione.
"""

from PIL import Image, ImageDraw

# Paleta de colores
SKIN = (210, 170, 130, 255)
SKIN_SHADOW = (180, 140, 100, 255)
HAIR = (60, 40, 30, 255)
HAIR_HIGHLIGHT = (90, 60, 40, 255)
EYES = (40, 40, 40, 255)
EYE_WHITE = (240, 240, 240, 255)
SHORTS_BLUE = (50, 80, 140, 255)
SHORTS_BLUE_SHADOW = (35, 60, 110, 255)
SUIT_BLACK = (30, 30, 35, 255)
SUIT_BLACK_SHADOW = (20, 20, 25, 255)
SUIT_LAPEL = (45, 45, 50, 255)
SHIRT_WHITE = (230, 230, 235, 255)
TIE_RED = (160, 30, 30, 255)
SHOES_BROWN = (80, 50, 30, 255)
SHOES_BLACK = (25, 25, 25, 255)
ROPE_BROWN = (140, 100, 50, 255)
ROPE_SHADOW = (110, 75, 35, 255)
TRANSPARENT = (0, 0, 0, 0)

# Colores items
PAPER_BG = (235, 220, 190, 255)
PAPER_SHADOW = (200, 185, 155, 255)
PAPER_LINE = (80, 60, 40, 255)
STAMP_RED = (180, 40, 40, 255)
SUIT_ITEM_BG = (35, 35, 40, 255)
SUIT_ITEM_LAPEL = (50, 50, 55, 255)
GUN_GREEN = (70, 110, 60, 255)
GUN_GREEN_DARK = (50, 80, 40, 255)
GUN_GREEN_LIGHT = (90, 135, 75, 255)
GUN_METAL = (100, 100, 105, 255)
GUN_HANDLE = (60, 40, 25, 255)
BULLET_GREEN = (80, 120, 65, 255)
BULLET_CASE = (180, 160, 60, 255)
BULLET_TIP = (140, 140, 145, 255)

FRAME_W = 40
FRAME_H = 56


def draw_base_character(draw, ox, oy, facing_right=True):
    """Dibuja el cuerpo base del personaje (piel, cara, pelo)."""
    # Head (centered around ox+20)
    cx = ox + 20
    head_top = oy + 8

    # Hair (top of head)
    for x in range(cx - 6, cx + 6):
        for y in range(head_top, head_top + 3):
            draw.point((x, y), HAIR)
    for x in range(cx - 7, cx + 7):
        draw.point((x, head_top + 1), HAIR)
    # Hair sides
    for y in range(head_top + 2, head_top + 8):
        draw.point((cx - 7, y), HAIR)
        draw.point((cx + 6, y), HAIR)
    # Hair highlight
    for x in range(cx - 4, cx + 2):
        draw.point((x, head_top + 1), HAIR_HIGHLIGHT)

    # Face / Head skin
    for x in range(cx - 6, cx + 6):
        for y in range(head_top + 3, head_top + 12):
            draw.point((x, y), SKIN)
    # Face shadow (jaw)
    for x in range(cx - 5, cx + 5):
        draw.point((x, head_top + 11), SKIN_SHADOW)

    # Eyes
    if facing_right:
        # Right-facing eyes
        draw.point((cx + 1, head_top + 6), EYE_WHITE)
        draw.point((cx + 2, head_top + 6), EYES)
        draw.point((cx + 1, head_top + 7), EYE_WHITE)
        draw.point((cx + 2, head_top + 7), EYES)
    else:
        draw.point((cx - 2, head_top + 6), EYE_WHITE)
        draw.point((cx - 3, head_top + 6), EYES)
        draw.point((cx - 2, head_top + 7), EYE_WHITE)
        draw.point((cx - 3, head_top + 7), EYES)

    # Mouth
    draw.point((cx + 1, head_top + 9), SKIN_SHADOW)
    draw.point((cx + 2, head_top + 9), SKIN_SHADOW)

    return head_top + 12  # Return neck Y


def draw_shirtless_body(draw, ox, oy, walk_offset=0):
    """Dibuja torso sin remera (piel) + shorts."""
    cx = ox + 20
    torso_top = oy + 20

    # Neck
    for x in range(cx - 3, cx + 3):
        for y in range(torso_top - 2, torso_top):
            draw.point((x, y), SKIN)

    # Torso (bare skin)
    for x in range(cx - 7, cx + 7):
        for y in range(torso_top, torso_top + 12):
            draw.point((x, y), SKIN)
    # Torso shadow edges
    for y in range(torso_top, torso_top + 12):
        draw.point((cx - 7, y), SKIN_SHADOW)
        draw.point((cx + 6, y), SKIN_SHADOW)
    # Chest shadow/definition
    for x in range(cx - 4, cx - 1):
        draw.point((x, torso_top + 3), SKIN_SHADOW)
    for x in range(cx + 1, cx + 4):
        draw.point((x, torso_top + 3), SKIN_SHADOW)
    # Belly button
    draw.point((cx, torso_top + 8), SKIN_SHADOW)

    # Arms (skin colored)
    arm_y = torso_top + 1
    # Left arm
    for y in range(arm_y, arm_y + 10):
        draw.point((cx - 8, y), SKIN)
        draw.point((cx - 9, y), SKIN)
        draw.point((cx - 10, y), SKIN_SHADOW)
    # Right arm
    for y in range(arm_y, arm_y + 10):
        draw.point((cx + 7, y), SKIN)
        draw.point((cx + 8, y), SKIN)
        draw.point((cx + 9, y), SKIN_SHADOW)
    # Hands
    for x in range(cx - 10, cx - 7):
        draw.point((x, arm_y + 10), SKIN)
    for x in range(cx + 7, cx + 10):
        draw.point((x, arm_y + 10), SKIN)

    shorts_top = torso_top + 12

    # Shorts (blue jean shorts)
    for x in range(cx - 7, cx + 7):
        for y in range(shorts_top, shorts_top + 7):
            draw.point((x, y), SHORTS_BLUE)
    # Shorts shadow
    for y in range(shorts_top, shorts_top + 7):
        draw.point((cx - 7, y), SHORTS_BLUE_SHADOW)
        draw.point((cx + 6, y), SHORTS_BLUE_SHADOW)
    # Shorts middle line (legs separation)
    for y in range(shorts_top + 3, shorts_top + 7):
        draw.point((cx, y), SHORTS_BLUE_SHADOW)

    legs_top = shorts_top + 7

    # Legs (skin)
    leg_offset = walk_offset
    for y in range(legs_top, legs_top + 8):
        # Left leg
        for x in range(cx - 5 + leg_offset, cx - 1 + leg_offset):
            if 0 <= x < ox + FRAME_W:
                draw.point((x, y), SKIN)
        # Right leg
        for x in range(cx + 1 - leg_offset, cx + 5 - leg_offset):
            if 0 <= x < ox + FRAME_W:
                draw.point((x, y), SKIN)

    feet_top = legs_top + 8
    # Shoes/feet
    for x in range(cx - 6 + leg_offset, cx - 1 + leg_offset):
        if 0 <= x < ox + FRAME_W:
            draw.point((x, feet_top), SHOES_BROWN)
            draw.point((x, feet_top + 1), SHOES_BROWN)
    for x in range(cx + 0 - leg_offset, cx + 5 - leg_offset):
        if 0 <= x < ox + FRAME_W:
            draw.point((x, feet_top), SHOES_BROWN)
            draw.point((x, feet_top + 1), SHOES_BROWN)


def draw_suited_body(draw, ox, oy, walk_offset=0):
    """Dibuja torso con traje smoking."""
    cx = ox + 20
    torso_top = oy + 20

    # Neck
    for x in range(cx - 3, cx + 3):
        for y in range(torso_top - 2, torso_top):
            draw.point((x, y), SKIN)

    # Suit torso
    for x in range(cx - 7, cx + 7):
        for y in range(torso_top, torso_top + 12):
            draw.point((x, y), SUIT_BLACK)
    # Lapels
    for y in range(torso_top, torso_top + 8):
        draw.point((cx - 4, y), SUIT_LAPEL)
        draw.point((cx - 3, y), SUIT_LAPEL)
        draw.point((cx + 2, y), SUIT_LAPEL)
        draw.point((cx + 3, y), SUIT_LAPEL)
    # White shirt visible between lapels
    for y in range(torso_top, torso_top + 10):
        draw.point((cx - 1, y), SHIRT_WHITE)
        draw.point((cx, y), SHIRT_WHITE)
        draw.point((cx + 1, y), SHIRT_WHITE)
    # Red tie
    for y in range(torso_top + 1, torso_top + 9):
        draw.point((cx, y), TIE_RED)
    # Tie knot
    draw.point((cx - 1, torso_top + 1), TIE_RED)
    draw.point((cx + 1, torso_top + 1), TIE_RED)
    # Suit shadow
    for y in range(torso_top, torso_top + 12):
        draw.point((cx - 7, y), SUIT_BLACK_SHADOW)
        draw.point((cx + 6, y), SUIT_BLACK_SHADOW)
    # Buttons
    draw.point((cx, torso_top + 5), SUIT_LAPEL)
    draw.point((cx, torso_top + 8), SUIT_LAPEL)

    # Suit arms
    arm_y = torso_top + 1
    for y in range(arm_y, arm_y + 10):
        draw.point((cx - 8, y), SUIT_BLACK)
        draw.point((cx - 9, y), SUIT_BLACK)
        draw.point((cx - 10, y), SUIT_BLACK_SHADOW)
        draw.point((cx + 7, y), SUIT_BLACK)
        draw.point((cx + 8, y), SUIT_BLACK)
        draw.point((cx + 9, y), SUIT_BLACK_SHADOW)
    # Hands (skin showing)
    for x in range(cx - 10, cx - 7):
        draw.point((x, arm_y + 10), SKIN)
    for x in range(cx + 7, cx + 10):
        draw.point((x, arm_y + 10), SKIN)

    pants_top = torso_top + 12

    # Suit pants
    for x in range(cx - 7, cx + 7):
        for y in range(pants_top, pants_top + 7):
            draw.point((x, y), SUIT_BLACK)
    for y in range(pants_top, pants_top + 7):
        draw.point((cx - 7, y), SUIT_BLACK_SHADOW)
        draw.point((cx + 6, y), SUIT_BLACK_SHADOW)
    for y in range(pants_top + 2, pants_top + 7):
        draw.point((cx, y), SUIT_BLACK_SHADOW)

    legs_top = pants_top + 7
    leg_offset = walk_offset

    # Suit legs
    for y in range(legs_top, legs_top + 8):
        for x in range(cx - 5 + leg_offset, cx - 1 + leg_offset):
            if 0 <= x < ox + FRAME_W:
                draw.point((x, y), SUIT_BLACK)
        for x in range(cx + 1 - leg_offset, cx + 5 - leg_offset):
            if 0 <= x < ox + FRAME_W:
                draw.point((x, y), SUIT_BLACK)

    feet_top = legs_top + 8
    # Black dress shoes
    for x in range(cx - 6 + leg_offset, cx - 1 + leg_offset):
        if 0 <= x < ox + FRAME_W:
            draw.point((x, feet_top), SHOES_BLACK)
            draw.point((x, feet_top + 1), SHOES_BLACK)
    for x in range(cx + 0 - leg_offset, cx + 5 - leg_offset):
        if 0 <= x < ox + FRAME_W:
            draw.point((x, feet_top), SHOES_BLACK)
            draw.point((x, feet_top + 1), SHOES_BLACK)


def create_npc_spritesheet(draw_body_func, filepath):
    """Crea un spritesheet de town NPC con 25 frames."""
    img = Image.new("RGBA", (FRAME_W, FRAME_H * 25), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    walk_offsets = [0, 0, 1, 1, 2, 2, 1, 1, 0, 0, -1, -1, -2, -2, -1]

    for frame in range(25):
        oy = frame * FRAME_H
        walk = walk_offsets[frame % len(walk_offsets)]

        # Draw character
        draw_base_character(draw, 0, oy, facing_right=True)
        draw_body_func(draw, 0, oy, walk_offset=walk if frame < 20 else 0)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_bound_frano(filepath):
    """Crea el sprite de Frano atado (1 frame)."""
    img = Image.new("RGBA", (FRAME_W, FRAME_H), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    draw_base_character(draw, 0, 0, facing_right=True)
    draw_shirtless_body(draw, 0, 0, walk_offset=0)

    cx = 20
    # Ropes around torso
    for x in range(cx - 8, cx + 8):
        draw.point((x, 24), ROPE_BROWN)
        draw.point((x, 25), ROPE_SHADOW)
        draw.point((x, 28), ROPE_BROWN)
        draw.point((x, 29), ROPE_SHADOW)
        draw.point((x, 32), ROPE_BROWN)
        draw.point((x, 33), ROPE_SHADOW)

    # Rope around legs
    for x in range(cx - 6, cx + 6):
        draw.point((x, 42), ROPE_BROWN)
        draw.point((x, 43), ROPE_SHADOW)

    # Rope knot on side
    for y in range(26, 30):
        draw.point((cx + 8, y), ROPE_BROWN)
        draw.point((cx + 9, y), ROPE_SHADOW)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_head_icon(filepath, suited=False):
    """Crea el icono de cabeza para el mapa (36x36)."""
    img = Image.new("RGBA", (36, 36), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    cx, cy = 18, 16

    # Hair
    for x in range(cx - 8, cx + 8):
        for y in range(cy - 9, cy - 5):
            draw.point((x, y), HAIR)
    for x in range(cx - 9, cx + 9):
        draw.point((x, cy - 7), HAIR)
    # Hair sides
    for y in range(cy - 6, cy + 2):
        draw.point((cx - 9, y), HAIR)
        draw.point((cx + 8, y), HAIR)

    # Face
    for x in range(cx - 8, cx + 8):
        for y in range(cy - 5, cy + 6):
            draw.point((x, y), SKIN)
    # Jaw shadow
    for x in range(cx - 7, cx + 7):
        draw.point((x, cy + 5), SKIN_SHADOW)

    # Eyes
    draw.point((cx + 1, cy - 1), EYE_WHITE)
    draw.point((cx + 2, cy - 1), EYES)
    draw.point((cx + 3, cy - 1), EYE_WHITE)
    draw.point((cx + 1, cy), EYE_WHITE)
    draw.point((cx + 2, cy), EYES)
    draw.point((cx + 3, cy), EYE_WHITE)

    # Mouth
    draw.point((cx + 1, cy + 3), SKIN_SHADOW)
    draw.point((cx + 2, cy + 3), SKIN_SHADOW)
    draw.point((cx + 3, cy + 3), SKIN_SHADOW)

    # Neck and collar hint
    for x in range(cx - 4, cx + 4):
        draw.point((x, cy + 7), SKIN)
        draw.point((x, cy + 8), SKIN)

    if suited:
        # Suit collar
        for x in range(cx - 6, cx + 6):
            draw.point((x, cy + 9), SUIT_BLACK)
            draw.point((x, cy + 10), SUIT_BLACK)
        # Lapels
        draw.point((cx - 3, cy + 9), SUIT_LAPEL)
        draw.point((cx + 2, cy + 9), SUIT_LAPEL)
        # Shirt
        draw.point((cx, cy + 9), SHIRT_WHITE)
        draw.point((cx, cy + 10), TIE_RED)
    else:
        # Bare neck/shoulders
        for x in range(cx - 6, cx + 6):
            draw.point((x, cy + 9), SKIN)
            draw.point((x, cy + 10), SKIN_SHADOW)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_orden_detencion(filepath):
    """Crea textura del item 'Orden de detención' (papel/documento)."""
    img = Image.new("RGBA", (32, 32), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    # Paper background
    for x in range(4, 28):
        for y in range(2, 30):
            draw.point((x, y), PAPER_BG)

    # Paper shadow (right and bottom edges)
    for y in range(3, 30):
        draw.point((28, y), PAPER_SHADOW)
    for x in range(5, 29):
        draw.point((x, 30), PAPER_SHADOW)

    # Fold corner (top right)
    for i in range(5):
        for j in range(5 - i):
            draw.point((28 - j, 2 + i), PAPER_SHADOW)

    # Text lines
    for x in range(7, 25):
        draw.point((x, 6), PAPER_LINE)
        draw.point((x, 10), PAPER_LINE)
    for x in range(7, 20):
        draw.point((x, 14), PAPER_LINE)
        draw.point((x, 18), PAPER_LINE)
    for x in range(7, 15):
        draw.point((x, 22), PAPER_LINE)

    # Red stamp/seal
    for x in range(18, 26):
        for y in range(20, 28):
            dist = abs(x - 22) + abs(y - 24)
            if dist <= 4:
                draw.point((x, y), STAMP_RED)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_el_traje(filepath):
    """Crea textura del item 'El Traje' (suit en percha)."""
    img = Image.new("RGBA", (32, 32), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    # Hanger
    for x in range(10, 22):
        draw.point((x, 3), GUN_METAL)
    draw.point((16, 1), GUN_METAL)
    draw.point((16, 2), GUN_METAL)
    # Hanger hook
    draw.point((15, 0), GUN_METAL)
    draw.point((17, 0), GUN_METAL)

    # Hanger slopes
    for i in range(4):
        draw.point((10 - i, 3 + i + 1), GUN_METAL)
        draw.point((21 + i, 3 + i + 1), GUN_METAL)

    # Suit jacket body
    for x in range(6, 26):
        for y in range(7, 24):
            draw.point((x, y), SUIT_ITEM_BG)

    # Lapels (V shape)
    for i in range(8):
        draw.point((14 - i // 2, 7 + i), SUIT_ITEM_LAPEL)
        draw.point((17 + i // 2, 7 + i), SUIT_ITEM_LAPEL)

    # White shirt between lapels
    for y in range(8, 22):
        draw.point((15, y), SHIRT_WHITE)
        draw.point((16, y), SHIRT_WHITE)

    # Red tie
    for y in range(9, 20):
        draw.point((15, y), TIE_RED)

    # Suit sleeves
    for y in range(8, 26):
        for x in range(4, 8):
            draw.point((x, y), SUIT_ITEM_BG)
        for x in range(24, 28):
            draw.point((x, y), SUIT_ITEM_BG)

    # Pants hint at bottom
    for x in range(8, 24):
        for y in range(24, 30):
            draw.point((x, y), SUIT_BLACK_SHADOW)
    # Pants separation
    for y in range(24, 30):
        draw.point((16, y), TRANSPARENT)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_pesto(filepath):
    """Crea textura del revólver 'Pesto' (verde gastado)."""
    img = Image.new("RGBA", (38, 24), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    # Barrel (long green part)
    for x in range(14, 36):
        for y in range(6, 11):
            draw.point((x, y), GUN_GREEN)
    # Barrel highlight (top)
    for x in range(16, 34):
        draw.point((x, 6), GUN_GREEN_LIGHT)
    # Barrel shadow (bottom)
    for x in range(14, 36):
        draw.point((x, 10), GUN_GREEN_DARK)
    # Barrel tip
    for y in range(7, 10):
        draw.point((36, y), GUN_METAL)

    # Cylinder (revolver drum)
    for x in range(12, 18):
        for y in range(4, 13):
            draw.point((x, y), GUN_GREEN)
    for y in range(5, 12):
        draw.point((12, y), GUN_GREEN_DARK)
    # Cylinder lines
    for x in range(13, 17):
        draw.point((x, 6), GUN_GREEN_DARK)
        draw.point((x, 9), GUN_GREEN_DARK)

    # Frame/body
    for x in range(6, 20):
        for y in range(8, 14):
            draw.point((x, y), GUN_GREEN)
    for x in range(6, 20):
        draw.point((x, 13), GUN_GREEN_DARK)

    # Trigger guard
    for x in range(10, 17):
        draw.point((x, 14), GUN_GREEN_DARK)
    draw.point((10, 15), GUN_GREEN_DARK)
    draw.point((16, 15), GUN_GREEN_DARK)
    for x in range(11, 16):
        draw.point((x, 16), GUN_GREEN_DARK)
    # Trigger
    draw.point((13, 14), GUN_METAL)
    draw.point((13, 15), GUN_METAL)

    # Handle/grip
    for x in range(6, 13):
        for y in range(14, 22):
            draw.point((x, y), GUN_HANDLE)
    # Handle texture lines
    for y in range(15, 21):
        draw.point((8, y), (75, 50, 30, 255))
        draw.point((10, y), (75, 50, 30, 255))
    # Handle bottom
    for x in range(5, 12):
        draw.point((x, 22), GUN_HANDLE)

    # Wear/scratch marks (green lighter spots)
    for pos in [(20, 7), (25, 8), (30, 7), (15, 5), (8, 10)]:
        draw.point(pos, GUN_GREEN_LIGHT)

    # Hammer
    draw.point((8, 5), GUN_METAL)
    draw.point((9, 5), GUN_METAL)
    draw.point((8, 4), GUN_METAL)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_bala_pesto(filepath):
    """Crea textura de la bala de Pesto."""
    img = Image.new("RGBA", (14, 14), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    # Bullet casing (bottom part)
    for x in range(4, 10):
        for y in range(7, 13):
            draw.point((x, y), BULLET_CASE)
    # Casing rim
    for x in range(3, 11):
        draw.point((x, 12), BULLET_CASE)

    # Bullet tip (green, top part)
    for x in range(5, 9):
        for y in range(3, 8):
            draw.point((x, y), BULLET_GREEN)
    for x in range(6, 8):
        for y in range(1, 4):
            draw.point((x, y), BULLET_GREEN)

    # Bullet point
    draw.point((6, 1), BULLET_TIP)
    draw.point((7, 1), BULLET_TIP)

    # Highlight
    draw.point((5, 4), GUN_GREEN_LIGHT)
    draw.point((5, 5), GUN_GREEN_LIGHT)

    img.save(filepath)
    print(f"  Created: {filepath}")


def create_pesto_projectile(filepath):
    """Crea textura del proyectil de Pesto."""
    img = Image.new("RGBA", (8, 8), TRANSPARENT)
    draw = ImageDraw.Draw(img)

    # Small bullet shape (horizontal, flying right)
    # Tip
    draw.point((6, 3), BULLET_TIP)
    draw.point((6, 4), BULLET_TIP)
    draw.point((7, 3), BULLET_TIP)
    draw.point((7, 4), BULLET_TIP)

    # Body (green)
    for x in range(2, 6):
        draw.point((x, 3), BULLET_GREEN)
        draw.point((x, 4), BULLET_GREEN)
    for x in range(3, 6):
        draw.point((x, 2), BULLET_GREEN)
        draw.point((x, 5), BULLET_GREEN)

    # Trail hint
    draw.point((1, 3), (BULLET_GREEN[0], BULLET_GREEN[1], BULLET_GREEN[2], 150))
    draw.point((1, 4), (BULLET_GREEN[0], BULLET_GREEN[1], BULLET_GREEN[2], 150))
    draw.point((0, 3), (BULLET_GREEN[0], BULLET_GREEN[1], BULLET_GREEN[2], 80))
    draw.point((0, 4), (BULLET_GREEN[0], BULLET_GREEN[1], BULLET_GREEN[2], 80))

    img.save(filepath)
    print(f"  Created: {filepath}")


def main():
    base = "/home/user/frano-mc/FranoMod"

    print("Generating FranoMod textures...")
    print()

    print("[NPCs]")
    create_npc_spritesheet(draw_shirtless_body, f"{base}/Content/NPCs/Frano.png")
    create_npc_spritesheet(draw_suited_body, f"{base}/Content/NPCs/Frano_Suited.png")
    create_bound_frano(f"{base}/Content/NPCs/BoundFrano.png")
    create_head_icon(f"{base}/Content/NPCs/Frano_Head.png", suited=False)

    print("[Items]")
    create_orden_detencion(f"{base}/Content/Items/OrdenDeDetencion.png")
    create_el_traje(f"{base}/Content/Items/ElTraje.png")
    create_pesto(f"{base}/Content/Items/Pesto.png")
    create_bala_pesto(f"{base}/Content/Items/BalaPesto.png")

    print("[Projectiles]")
    create_pesto_projectile(f"{base}/Content/Projectiles/PestoProjectile.png")

    print()
    print("All textures generated successfully!")


if __name__ == "__main__":
    main()
