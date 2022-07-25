# Unity Mirror Networking Skeleton

## Server configuration

### Example docker-compose

Docker-compose file:
```yaml
unity-relay:
  container_name: unity-relay
  image: derekrs/lrm_node:Bleeding-Edge
  ports:
    - '7776:7776/udp'
    - '7777:7777/udp'
    - '8080:8080'
  volumes:
    - './config/:/config/'
    - '/etc/timezone:/etc/timezone:ro'
  tty: true
  restart: always
```

Config file in config/config.json:
```json
{
  "TransportDLL": "MultiCompiled.dll",
  "TransportClass": "kcp2k.KcpTransport",
  "AuthenticationKey": "porno",
  "TransportPort": 7777,
  "UpdateLoopTime": 10,
  "UpdateHeartbeatInterval": 100,
  "UseEndpoint": true,
  "EndpointPort": 8080,
  "EndpointServerList": true,
  "EnableNATPunchtroughServer": true,
  "NATPunchtroughPort": 7776,
  "UseLoadBalancer": false,
  "LoadBalancerAuthKey": "AuthKey",
  "LoadBalancerAddress": "127.0.0.1",
  "LoadBalancerPort": 7070,
  "LoadBalancerRegion": 1
}

```
