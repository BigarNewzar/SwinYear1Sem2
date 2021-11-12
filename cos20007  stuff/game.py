import pygame, sys, math, random
from pygame.locals import *
from statistics import mean

pygame.init()

SCREEN_WIDTH = 640
SCREEN_HEIGHT = 480
DISPLAYSURF = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))

GREEN = (0, 255, 0)
RED = (255, 0, 0)
WHITE = (255, 255, 255)

pygame.display.set_caption('Asteroids')

rotate_left = False
rotate_right = False
bullets = []
asteroids = []

class ship:
    def __init__(self):
        self.pos = [SCREEN_WIDTH/2,SCREEN_HEIGHT/2]
        self.direc = 0
        self.size = 20
        self.engine_on = False
        self.hidden = True
        self.alive = True
        self.speed = [0, 0]
        self.invs_time = 7000

    def draw(self):
        mod = 7/12
        player_vert = (
            (self.pos[0]+self.size*math.cos(self.direc+math.pi*(2/3)),
            self.pos[1]+self.size*math.sin(self.direc+math.pi*(2/3))),
            (self.pos[0]+self.size*math.cos(self.direc),
            self.pos[1]+self.size*math.sin(self.direc)),
            (self.pos[0]+self.size*math.cos(self.direc-math.pi*(2/3)),
            self.pos[1]+self.size*math.sin(self.direc-math.pi*(2/3)))
            )
        pygame.draw.lines(DISPLAYSURF, WHITE, False, player_vert, 3)
        pygame.draw.line(DISPLAYSURF, WHITE,
            (self.pos[0]+15*math.cos(self.direc+math.pi*mod),
             self.pos[1]+15*math.sin(self.direc+math.pi*mod)),
            (self.pos[0]+15*math.cos(self.direc-math.pi*mod),
             self.pos[1]+15*math.sin(self.direc-math.pi*mod)), 3)

        if self.engine_on:
            pygame.draw.ellipse(DISPLAYSURF, RED,
                    (self.pos[0]+10*math.cos(self.direc+math.pi)-7,
                    self.pos[1]+10*math.sin(self.direc+math.pi)-7, 15, 15), 5)

    def update(self):
        if self.engine_on:
            self.speed[0] += math.cos(self.direc)/10000
            self.speed[1] += math.sin(self.direc)/10000

        self.pos[0] += self.speed[0]
        self.pos[1] += self.speed[1]
       
        self.pos[0] = self.pos[0] % (SCREEN_WIDTH + 30)
        self.pos[1] = self.pos[1] % (SCREEN_HEIGHT + 30)

    def rotate(self, angle):
        self.direc += angle

    def check(self, asteroid):
        if (asteroid.pos[0] + mean(asteroid.rand_points) >
            self.pos[0] - self.size and
            asteroid.pos[0] - mean(asteroid.rand_points) <
            self.pos[0] + self.size and
            asteroid.pos[1] + mean(asteroid.rand_points) >
            self.pos[1] - self.size and
            asteroid.pos[1] + mean(asteroid.rand_points) <
            self.pos[1] + self.size):
                        self.alive = False
                        print('you lost :(')

class bullet:
    def __init__(self, x, y, d):
        self.pos = [x, y]
        self.direc = d
        self.inb = True

    def update(self):
        pygame.draw.ellipse(DISPLAYSURF, WHITE, (
            self.pos[0],
            self.pos[1],
            5, 5), 2)

        self.pos[0] += math.cos(self.direc) 
        self.pos[1] += math.sin(self.direc)

    def inbounds(self):
        if self.pos[0] < 0 or self.pos[1] > SCREEN_WIDTH:
            self.inb = False
        elif self.pos[1] < 0 or self.pos[1] > SCREEN_HEIGHT:
            self.inb =  False
        return self.inb

class asteroid:
    def __init__(self, x, y, s):
        self.in_game = False
        self.direc = random.randint(0,6)
        self.pos = [x, y]
        self.size = s
        self.broke = False
        self.rand_points = [random.randint(20,80//s) for i in range(10//s)]

    def draw(self):
        astr_vert = []
        for i in range(len(self.rand_points)-1):
            da = i * 2*math.pi / (len(self.rand_points)-1)
            astr_vert.append(
            (self.pos[0]+self.rand_points[i]*math.cos(self.direc+da),
             self.pos[1]+self.rand_points[i]*math.sin(self.direc+da))
            )
        pygame.draw.polygon(DISPLAYSURF, WHITE, astr_vert, 3)

        self.pos[0] += math.cos(self.direc)/80
        self.pos[1] += math.sin(self.direc)/80

        self.pos[0] = self.pos[0] % (SCREEN_WIDTH + 30)
        self.pos[1] = self.pos[1] % (SCREEN_HEIGHT + 30)

    def update(self, bullet):
        if bullet.pos[0] > self.pos[0] - max(self.rand_points):
            if bullet.pos[0] < self.pos[0] + max(self.rand_points):
                if bullet.pos[1] > self.pos[1] - max(self.rand_points):
                    if bullet.pos[1] < self.pos[1] + max(self.rand_points):
                        bullet.inb = False
                        self.hit()
    def hit(self):
        if self.size < 3:
            asteroids.append(asteroid(self.pos[0], self.pos[1], self.size+1))
            asteroids.append(asteroid(self.pos[0], self.pos[1], self.size+1))

        self.broke = True

player = ship()

for i in range(0, 8):
    asteroids.append(asteroid(random.randint(0, SCREEN_WIDTH),
                             random.randint(0, SCREEN_HEIGHT),
                             random.randint(1,2)
                             ))

while player.alive:
    for event in pygame.event.get():
        if event.type == QUIT:
            pygame.quit()
            sys.exit()
        if event.type == KEYDOWN:
            if event.key == K_a:
                rotate_left = True            
            elif event.key == K_d:
                rotate_right = True
            elif event.key == K_w:
                player.engine_on = True
        if event.type == KEYUP:
            if event.key == K_a:
                rotate_left = False            
            elif event.key == K_d:
                rotate_right = False
            elif event.key == K_w:
                player.engine_on = False
            elif event.key == K_SPACE:
                bullets.append(
                        bullet(player.pos[0], player.pos[1], player.direc)
                        )

    if rotate_left:
        player.rotate(-0.0015)
    elif rotate_right:
        player.rotate(0.0015)
    
    DISPLAYSURF.fill((0, 0, 0))

    if player.invs_time % 10 == 0:
        player.draw()
    if player.invs_time > 0:
        player.invs_time -= 1
    else:
        player.hidden = False

    player.update()

    for astr in asteroids:
        player.check(astr)
        astr.draw()
        if len(bullets) > 0:
            for ammo in bullets:
                astr.update(ammo)
                if astr.broke:
                    asteroids.remove(astr)
                    del astr

    for ammo in bullets:
        ammo.update()
        if not ammo.inbounds():
             bullets.remove(ammo)
             del ammo

    if len(asteroids) < 1:
        player.alive = False
        print('you won!')

    pygame.display.update()
