#pragma strict
 
private var g : float;
 
function OnEnable ()
{
    g = rigidbody2D.gravityScale;
    rigidbody2D.gravityScale = 0;
}
 
function Update () 
{
    rigidbody2D.velocity = Vector3.zero;
    rigidbody2D.angularVelocity = 0;
}
 
function OnDisable () {
    rigidbody2D.gravityScale = g;
}