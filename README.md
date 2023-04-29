# ProtocolBuilder

통신을 위한 패킷을 xml로 정의하여 각 언어에 맞게 클래스를 생성한다.

현재 CPP만 적용되어있다.
다른 언어로 패킷을 정의하고 싶다면 IGenerator를 상속받아 정의하고 사용하면된다.

# xml 정의 예시
## 정의
 ```
  <packet>                                      //패킷 정의 시작  
    <packet_dual name="Test" src="C" dst="S">   //양방향 패킷  request response 가 짝이다.  
      <request>                                 //request 정의  
        <field name="t1" type="int32" />  
      </request>  
      <response>                                //response 정의  
        <field name="t1" type="int32" />  
        <field name="t2" type="int32" />  
      </response>  
    </packet_dual>  
    <packet_one name="Ack" src="C" dst="S">    //단방향 패킷 information 이다.  
      <information>                            //information 정의  
        <field name="t3" type="int32" />  
      </information>  
    </packet_one>  
  </packet>  
```
## 결과
```
  struct request_C_S_Test  
  {  
    int32 t1;  
  }  
  
  struct response_S_C_Test  
  {  
    int32 t1;  
    int32 t2;  
  }  
  
  struct information_C_S_Ack  
  {  
    int32 t3;  
  }  
```

# 수정해야할 부분
1. field의 type을 구분할 방법을 마련해야한다.
2. 클래스에 기능이 들어 갈 수도 있다. 그 때 정렬 방법에 대해서도..
