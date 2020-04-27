from matplotlib import pyplot as plt
from platypus import ZDT1, ZDT2, ZDT3
from platypus.algorithms import NSGAII, GDE3, SPEA2
from platypus.operators import UniformMutation
from pymoo.factory import get_problem

from argument_parser import ArgumentParser


args = ArgumentParser().get_arguments()

if args.test_problem is 1:
    test_problem = ZDT1()
elif args.test_problem is 2:
    test_problem = ZDT2()
elif args.test_problem is 3:
    test_problem = ZDT3()

variator = UniformMutation(probability=args.probability,
                           perturbation=0.5)

if args.algorithm is 1:
    algorithm = NSGAII(test_problem, args.population, variator=variator)
elif args.algorithm is 2:
    algorithm = GDE3(test_problem, args.population, variator=variator)
elif args.algorithm is 3:
    algorithm = SPEA2(test_problem, args.population, variator=variator)

algorithm.run(args.generations)

pareto = get_problem(type(test_problem).__name__).pareto_front()

plt.scatter([s.objectives[0] for s in algorithm.result],
            [s.objectives[1] for s in algorithm.result],
            color='green', label='Rozwiązania', s=10)
plt.scatter(pareto[:, 0], pareto[:, 1], color='blue', s=10, label='Front Pareto')
plt.grid(alpha=0.10)
plt.legend()

file_name = type(algorithm).__name__ + '_' + type(test_problem).__name__ + \
            '_g' + str(args.generations) + '_p' + str(args.population) + \
            '_r' + str(args.probability).replace('.', ',') 

plt.savefig(file_name + '.png', dpi=300)
