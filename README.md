# CalmattaSolutions

This solution is implemented using RabbitMq and Docker.

**Calmatta.ChatClient** is the starting point of applciation. User initiates a chat here.

**Calmatta.ResourceServerAPI** is the API, we can also test these API using Swagger endpoints.

**Calmatta.ChatMonitorClient** is the monitoring service. It connects RabbitMQ server and pass the message to the team. Messages are stored in FIFO. It shows team member has assigned which chat according to capacity.

**Calmatta.DAL** is the DataAccessLayer which performs business logic from all the service. Here it checks the logic for Agents and teams avaibility. 
Each agent has work capacity with team members (junior, mid-level, senior, team lead). According to shift capacity agent assign the chat to the team members automatically by calculating capcity.

**Calmatta.UnitTest** it performs unit test for the above Business logic.
