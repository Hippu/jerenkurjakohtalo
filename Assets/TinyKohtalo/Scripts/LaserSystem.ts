
namespace game {

    /** New System */
    @ut.executeAfter(ut.Shared.UserCodeStart)
    @ut.executeBefore(ut.Shared.UserCodeEnd)
    export class LaserSystem extends ut.ComponentSystem {

        OnUpdate(): void {
            this.world.forEach([LaserShooter], shooter => {
                switch (shooter.shootMode) {
                    case ShootingMode.Random:
                        this.ShootRandom(shooter);
                        break;

                    default:
                        break;
                }
            })
            this.MoveLasers();
        }

        ShootRandom(shooter: LaserShooter): void {
            let now = this.world.scheduler().now();
            if (now - shooter.lastShotOn > 0.25) {
                let spawnerEntity =
                    shooter.laserSpawners[shooter.laserSpawnerIndex]
                shooter.laserSpawnerIndex += 1;

                if (shooter.laserSpawnerIndex >= shooter.laserSpawners.length) {
                    shooter.laserSpawnerIndex = 0;
                }
                let laser = ut.EntityGroup.instantiate(this.world, 'game.laser')[0];
                this.world.usingComponentData(laser, [ut.Core2D.TransformLocalPosition, ut.Core2D.TransformLocalRotation], (pos, rot) => {
                    pos.position = ut.Core2D.TransformService.computeWorldPosition(this.world, spawnerEntity);
                    rot.rotation.z += (Math.random() - 0.5) * (Math.PI / 5)
                })

                shooter.lastShotOn = now;
            }
        }

        MoveLasers(): void {
            let dt = this.world.scheduler().deltaTime();
            this.world.forEach(
                [ut.Entity, Laser, ut.Core2D.TransformLocalPosition, ut.Core2D.TransformLocalRotation],
                (entity, laser, position, rotation) => {
                    let rot = new Euler().setFromQuaternion(rotation.rotation);
                    let headingVector = new Vector3(-Math.sin(rot.z), Math.cos(rot.z), 0).negate();
                    let newPosition = position.position.add(headingVector.multiplyScalar(dt * laser.speed));
                    position.position = newPosition;
                    let xpos: number, ypos: number = (position.position.x, position.position.y);
                    if (ypos < -6 || xpos < 5 || xpos > 5) {
                        this.world.destroyEntity(entity);
                    }
                })
        }
    }
}
